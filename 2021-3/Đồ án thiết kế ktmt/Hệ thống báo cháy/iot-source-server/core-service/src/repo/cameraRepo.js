module.exports = container => {
    const { schemas } = container.resolve('models')
    const { Camera } = schemas
    const addCamera = (data) => {
        const c = new Camera(data)
        return c.save()
    }
    const getCameraById = (id) => {
        return Camera.findById(id)
    }
    const deleteCamera = (id) => {
        return Camera.findByIdAndRemove(id, { useFindAndModify: false })
    }
    const updateCamera = (id, n) => {
        return Camera.findByIdAndUpdate(id, n, {
            useFindAndModify: false,
            returnOriginal: false
        })
    }
    const checkIdExist = (id) => {
        return Camera.findOne({ id })
    }
    const getCount = (pipe = {}) => {
        return Camera.countDocuments(pipe)
    }
    const getCameraAgg = (pipe) => {
        return Camera.aggregate(pipe)
    }
    const getCamera = (pipe, limit, skip, sort) => {
        return Camera.find(pipe).limit(limit).skip(skip).sort(sort)
    }
    const getCameraNoPaging = (pipe) => {
        return Camera.find(pipe)
    }
    const removeCamera = (pipe) => {
        return Camera.deleteMany(pipe)
    }
    return {
        getCameraNoPaging,
        removeCamera,
        addCamera,
        getCameraAgg,
        getCameraById,
        deleteCamera,
        updateCamera,
        checkIdExist,
        getCount,
        getCamera
    }
}