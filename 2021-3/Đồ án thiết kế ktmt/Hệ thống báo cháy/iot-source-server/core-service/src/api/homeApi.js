module.exports = (app, container) => {
  const { serverSettings } = container.resolve('config')
  const { homeController } = container.resolve('controller')
  const { basePath } = serverSettings
  app.get(`${basePath}/home`, homeController.getHome)
  app.get(`${basePath}/home/:id`, homeController.getHomeById)
  app.put(`${basePath}/home/:id`, homeController.updateHome)
  app.delete(`${basePath}/home/:id`, homeController.deleteHome)
  app.post(`${basePath}/home`, homeController.addHome)
}