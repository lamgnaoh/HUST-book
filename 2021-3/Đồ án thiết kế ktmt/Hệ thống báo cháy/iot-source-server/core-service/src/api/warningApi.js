module.exports = (app, container) => {
    const { serverSettings } = container.resolve('config')
    const { warningController } = container.resolve('controller')
    const { basePath } = serverSettings
    app.get(`${basePath}/warning`, warningController.getWarning)
    app.get(`${basePath}/warning/:id`, warningController.getWarningById)
    app.put(`${basePath}/warning/:id`, warningController.updateWarning)
    app.delete(`${basePath}/warning/:id`, warningController.deleteWarning)
    app.post(`${basePath}/warning`, warningController.addWarning)
}