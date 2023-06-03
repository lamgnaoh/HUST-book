// const cluster = require('cluster')
const mqemitter = require('mqemitter-mongodb')
const mongoPersistence = require('aedes-persistence-mongodb')
const request = require('request-promise')

module.exports = (container) => {
  const logger = container.resolve('logger')
  const MONGO_URL_MQTT = container.resolve('connectionStringAedes')
  const {
    httpCode,
    urlConfig,
    serverHelper,
    serverMqttSettings,
    workerConfig: { queueName }
  } = container.resolve('config')
  const firebaseAdmin = container.resolve('firebaseAdmin')
  const redisHelper = container.resolve('redisHelper')
  const publisher = container.resolve('publisher')
  const { homeRepo, deviceRepo, roomRepo } = container.resolve('repo')
  const start = () => {
    function startAedes () {
      const ws = require('websocket-stream')
      const port = serverMqttSettings.port
      const webPort = serverMqttSettings.webPort
      const aedes = require('aedes')({
        id: 'MQTT_BROKER',
        mq: mqemitter({
          url: MONGO_URL_MQTT
        }),
        persistence: mongoPersistence({
          url: MONGO_URL_MQTT,
          // Optional ttl settings
          ttl: {
            packets: 300, // Number of seconds
            subscriptions: 300
          }
        }),
        connectTimeout: 50000
      })

      const wsServer = require('http').createServer()
      ws.createServer({
        server: wsServer
      }, aedes.handle).on('error', (err) => {
        console.log(err.message)
      })

      const server = require('net').createServer(aedes.handle)

      server.listen(port, function () {
        console.log('Aedes listening on port:', port)
      })

      wsServer.listen(webPort, '0.0.0.0', function () {
        console.log('MQTT websocket server listening on port', webPort)
      })

      aedes.authenticate = async (client, username, password, callback) => {
        password = (password || '').toString()
        if (username.toString() === password.toString()) {
          client.token = {
            username,
            password,
            id: client.id,
            scope: 'aedes-write aedes-read'
          }
          return callback(null, true)
        }
        return callback(new Error('User not found'), false)
      }

      function checkAnyScope (client, ...requiredScopes) {
        if (typeof client.token.scope !== 'string') {
          throw new TypeError('Token contains no scope')
        }
        const tokenScopes = client.token.scope.split(' ')

        for (const requiredScope of requiredScopes) {
          if (tokenScopes.includes(requiredScope)) {
            return
          }
        }
        throw new Error('Insufficient to permissions to publish message')
      }

      // publish LWT to all online clients
      aedes.authorizePublish = (client, packet, callback) => {
        // Using LWT for check connect error
        if (client.token instanceof Object) {
          try {
            checkAnyScope(client, 'aedes-write')
            return callback(null)
          } catch (error) {
            return callback(error)
          }
        }
        callback(new Error('Cannot publish'))
      }
      aedes.authorizeSubscribe = (client, subscription, callback) => {
        if (client.token instanceof Object) {
          try {
            checkAnyScope(client, 'aedes-read')
            return callback(null, subscription)
          } catch (error) {
            console.log('author failed')
            return callback(error)
          }
        }
        callback(new Error('Cannot subscribe'))
      }
      aedes.preConnect = (client, packet, callback) => {
        // check client connect over limit
        // if(aedes.connectedClient > 10000){
        //   :)))
        // }
        // const IP = client.conn.remoteAddress
        // if(IP = '8.8.8.8') {
        //   callback(new Error('Google', false))
        // }
        callback(null, true)
      }
      aedes.on('connectionError', function (client, error) {
        console.log(`${client} connect error ${error}`)
        logger.e(error)
      })
      // Emitted when an error occurs.
      aedes.on('clientError', function (client, error) {
        console.log(`${client} ${error}`)
      })
      // Emitted when the client registers itself to server. Connecting = true
      aedes.on('client', function (client) {
        // console.log('Client Connecting: \x1b[33m' + (client ? client.id : client) + '\x1b[0m', 'to broker', aedes.id)
      })
      // handle online status of client
      // The client connected state equals to true and is ready for processing incoming messages.
      aedes.on('clientReady', async function (client) {
        console.log('Client Online: \x1b[33m' + (client ? client.id : client) + '\x1b[0m', 'to broker', aedes.id)
      })
      // handle offline status of client
      aedes.on('clientDisconnect', function (client) {
        console.log('Client Offline: \x1b[31m' + (client ? client.id : client) + '\x1b[0m', 'to broker', aedes.id)
      })

      aedes.on('ping', async function (packet, client) {

      })
      aedes.on('ack', (packet, client) => {
        // undefine packer ? why?
        // const ackKey = `${client.token._id.toString()}`
        // redisHelper.set(ackKey, JSON.stringify(serverHelper.handleDataBeforeCache('ack')))
      })

      aedes.on('connackSent', async function (packet, client) {
        // connack packet for ack connect success
      })
      // Emitted when timeout happes in the client keepalive.
      aedes.on('keepaliveTimeout', function (client) {
        console.log('Client timeout')
      })
      aedes.on('subscribe', function (subscriptions, client) {
        console.log('MQTT client \x1b[32m' + (client ? client.id : client) +
            '\x1b[0m subscribed to topics: ' + subscriptions.map(s => s.topic).join('\n'), 'from broker', aedes.id)
      })

      aedes.on('unsubscribe', function (subscriptions, client) {
        console.log('MQTT client \x1b[32m' + (client ? client.id : client) +
            '\x1b[0m unsubscribed to topics: ' + subscriptions.join('\n'), 'from broker', aedes.id)
      })
      // fired when a message is published
      aedes.on('publish', async function (packet, client) {
        const topic = packet.topic
        if (topic.startsWith('$SYS')) return // System message
        if (client) {
          let message = JSON.parse(packet.payload.toString())
          const home = await homeRepo.getHomeById(message.home)
          if (home) {
            const device = await deviceRepo.getDeviceById(message.device)
            if (device) {
              await publisher.sendToQueue({ "content": "Fake" }, queueName)
              const warning = {
                content: `Cảnh báo nguy hiểm, tại nhà ${home.name}, Phòng của ${device.room.owner}, tầng ${device.room.floor},
                vị trí ${device.room.position}`,
                device: device._id.toString(),
                seen: 0
              }
              const notification = {
                data: {
                  description: warning.content,
                  user_name: "iotCommunication"
                },
                tokens: ["dRD01wYmQX2d6llgkkyrfp:APA91bHw_3qP7iir0HWe5EyOgukvhbPuQPUVcE-yM0-DlYEAu5pq0gVhkiHrUIu2ZfdCqGtC5stJkcMLvcRZRq4esXNMb7ujLJX8xLuHc2QPooPsvv_kD6cWXwIflhnsNbpZdEdWlLkY"]
              }
              await firebaseAdmin.messaging().sendMulticast(notification)
                  .then(response => {
                    console.log('Successfully sent message:', response)
                  })
                  .catch(error => {
                    console.log('Error sending message:', error)
                  })
              aedes.publish({ topic: home.user + 'rece', payload: Buffer.from(JSON.stringify(warning)), qos: 1 })

            }
          }
        }
        return
      })
    }

    // extras feature checking user online offline
    // if (cluster.isMaster) {
    //   const numWorkers = require('os').cpus().length
    //   for (let i = 0; i < numWorkers; i++) {
    //     cluster.fork()
    //   }
    //   cluster.on('online', function (worker) {
    //     console.log('Worker ' + worker.process.pid + ' is online')
    //   })
    //
    //   cluster.on('exit', function (worker, code, signal) {
    //     console.log('Worker ' + worker.process.pid + ' died with code: ' + code + ', and signal: ' + signal)
    //     console.log('Starting a new worker')
    //     cluster.fork()
    //   })
    // } else {
    //   startAedes()
    // }
    startAedes()
  }
  return { start }
}
