module.exports = (container) => {
  const cameraController = require('./cameraController')(container)
  const homeController = require('./homeController')(container)
  const roomController = require('./roomController')(container)
  const smokeSensorController = require('./smokeSensorController')(container)
  const deviceController = require('./deviceController')(container)
  const warningController = require('./warningController')(container)
  return { cameraController, homeController, roomController, smokeSensorController, deviceController, warningController }
}
