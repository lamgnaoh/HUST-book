const repo = (container) => {
  const cameraRepo = require('./cameraRepo')(container)
  const homeRepo = require('./homeRepo')(container)
  const roomRepo = require('./roomRepo')(container)
  const smokeSensorRepo = require('./smokeSensorRepo')(container)
  const deviceRepo = require('./deviceRepo')(container)
  const warningRepo = require('./warningRepo')(container)
  return { cameraRepo, homeRepo, roomRepo, smokeSensorRepo, deviceRepo, warningRepo }
}
const connect = (container) => {
  const dbPool = container.resolve('db')
  if (!dbPool) throw new Error('Connect DB failed')
  return repo(container)
}

module.exports = { connect }
