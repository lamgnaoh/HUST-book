module.exports = (app, container) => {
  const { serverSettings } = container.resolve('config')
  const { cameraController } = container.resolve('controller')
  const { basePath } = serverSettings
  app.get(`${basePath}/camera`, cameraController.getCamera)
  app.get(`${basePath}/camera/:id`, cameraController.getCameraById)
  app.put(`${basePath}/camera/:id`, cameraController.updateCamera)
  app.delete(`${basePath}/camera/:id`, cameraController.deleteCamera)
  app.post(`${basePath}/camera`, cameraController.addCamera)
}