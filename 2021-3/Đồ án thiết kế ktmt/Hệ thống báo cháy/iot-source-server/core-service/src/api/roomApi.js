module.exports = (app, container) => {
  const { serverSettings } = container.resolve('config')
  const { roomController } = container.resolve('controller')
  const { basePath } = serverSettings
  app.get(`${basePath}/room`, roomController.getRoom)
  app.get(`${basePath}/room/:id`, roomController.getRoomById)
  app.put(`${basePath}/room/:id`, roomController.updateRoom)
  app.delete(`${basePath}/room/:id`, roomController.deleteRoom)
  app.post(`${basePath}/room`, roomController.addRoom)
}