module.exports = (app, container) => {
    const { serverSettings } = container.resolve('config')
    const { deviceController } = container.resolve('controller')
    const { basePath } = serverSettings
    app.get(`${basePath}/device`, deviceController.getDevice)
    app.get(`${basePath}/device/:id`, deviceController.getDeviceById)
    app.put(`${basePath}/device/:id`, deviceController.updateDevice)
    app.delete(`${basePath}/device/:id`, deviceController.deleteDevice)
    app.post(`${basePath}/device`, deviceController.addDevice)
}