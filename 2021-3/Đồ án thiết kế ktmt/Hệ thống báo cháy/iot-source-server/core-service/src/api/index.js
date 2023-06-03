module.exports = (app, container) => {
  const { verifyAccessToken } = container.resolve('middleware')
  app.use(verifyAccessToken)
  require('./cameraApi')(app, container)
  require('./homeApi')(app, container)
  require('./roomApi')(app, container)
  require('./deviceApi')(app, container)
  require('./smokeSensorApi')(app, container)
  require('./warningApi')(app, container)
}