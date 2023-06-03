const { initDI } = require('./di')
const { name } = require('../package.json')
const config = require('./config')
const logger = require('./logger')
const middleware = require('./middleware')
const firebaseAdmin = require('firebase-admin')
const aedes = require('./aedes')
const { start } = require('./cache')
const updateCache = require('./updateCache')
const { Subscriber, createChannel, Publisher } = require('./rabbitMQ')
const server = require('./server')
const redisHelper = require('./redisHelper')
const models = require('./models')
const controller = require('./controller')
const { connect } = require('./database')
const repo = require('./repo')
const EventEmitter = require('events').EventEmitter
const mediator = new EventEmitter()
logger.d(`${name} Service`)
mediator.once('di.ready', async container => {
  console.log('di.ready, starting connect db ', config.dbSettings)
  container.registerValue('config', config)
  container.registerValue('middleware', middleware)
  container.registerValue('logger', logger)
  container.registerValue('mediator', mediator)

  // firebase connect
  firebaseAdmin.initializeApp({
    credential: firebaseAdmin.credential.cert(config.firebaseConfig.serviceAccountPath)
    // databaseURL: config.firebaseConfig.databaseURL
  })
  console.log('connected firebase ', config.firebaseConfig)
  container.registerValue('firebaseAdmin', firebaseAdmin)

  // for rabbitMQ
  const channel = await createChannel(config.rabbitConfig)
  const publisher = new Publisher(channel, config.workerConfig.exchange)
  container.registerValue('publisher', publisher)

  mediator.once('db.ready', async db => {
    logger.d('db.ready, starting server')
    container.registerValue('db', db)
    container.registerValue('models', models(container))
    const repository = repo.connect(container)
    container.registerValue('repo', repository)

    const cache = await start(container)
    logger.d('redis.ready, starting server')
    container.registerValue('redis', cache)
    container.registerValue('redisHelper', redisHelper(container))

    container.registerValue('controller', controller(container))
    container.registerValue('middleware', middleware(container))
    aedes(container).start()
    server.start(container).then(app => {
      logger.d('Server started at port ', app.address().port)
    })
  })
  connect(container, mediator)
})
initDI(mediator)
