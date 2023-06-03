module.exports = container => {
    const { schemas } = container.resolve('models')
    const { Device } = schemas
    const addDevice = (data) => {
        const n = new Device(data)
        return n.save()
    }
    const getDeviceById = (id) => {
        return Device.findById(id).populate('room')
    }
    const deleteDevice = (id) => {
        return Device.findByIdAndRemove(id, { useFindAndModify: false })
    }
    const updateDevice = (id, n) => {
        return Device.findByIdAndUpdate(id, n, {
            useFindAndModify: false,
            returnOriginal: false
        })
    }
    const checkIdExist = (id) => {
        return Device.findOne({ id })
    }
    const getCount = (pipe = {}) => {
        return Device.countDocuments(pipe)
    }
    const getDeviceAgg = (pipe) => {
        return Device.aggregate(pipe)
    }
    const getDevice = (pipe, limit, skip, sort) => {
        return Device.find(pipe).limit(limit).skip(skip).sort(sort)
    }
    const getDeviceNoPaging = (pipe) => {
        return Device.find(pipe)
    }
    const removeDevice = (pipe) => {
        return Device.deleteMany(pipe)
    }
    return {
        getDeviceNoPaging,
        removeDevice,
        addDevice,
        getDeviceAgg,
        getDeviceById,
        deleteDevice,
        updateDevice,
        checkIdExist,
        getCount,
        getDevice
    }
}