module.exports = (app, container) => {
    const { serverSettings } = container.resolve('config')
    const { smokeSensorController } = container.resolve('controller')
    const { basePath } = serverSettings
    app.get(`${basePath}/smokeSensor`, smokeSensorController.getSmokeSensor)
    app.get(`${basePath}/smokeSensor/:id`, smokeSensorController.getSmokeSensorById)
    app.put(`${basePath}/smokeSensor/:id`, smokeSensorController.updateSmokeSensor)
    app.delete(`${basePath}/smokeSensor/:id`, smokeSensorController.deleteSmokeSensor)
    app.post(`${basePath}/smokeSensor`, smokeSensorController.addSmokeSensor)
}