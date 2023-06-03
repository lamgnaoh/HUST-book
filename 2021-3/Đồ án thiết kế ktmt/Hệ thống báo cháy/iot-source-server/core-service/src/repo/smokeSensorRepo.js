module.exports = container => {
    const { schemas } = container.resolve('models')
    const { SmokeSensor } = schemas
    const addSmokeSensor = (data) => {
        const n = new SmokeSensor(data)
        return n.save()
    }
    const getSmokeSensorById = (id) => {
        return SmokeSensor.findById(id)
    }
    const deleteSmokeSensor = (id) => {
        return SmokeSensor.findByIdAndRemove(id, { useFindAndModify: false })
    }
    const updateSmokeSensor = (id, n) => {
        return SmokeSensor.findByIdAndUpdate(id, n, {
            useFindAndModify: false,
            returnOriginal: false
        })
    }
    const checkIdExist = (id) => {
        return SmokeSensor.findOne({ id })
    }
    const getCount = (pipe = {}) => {
        return SmokeSensor.countDocuments(pipe)
    }
    const getSmokeSensorAgg = (pipe) => {
        return SmokeSensor.aggregate(pipe)
    }
    const getSmokeSensor = (pipe, limit, skip, sort) => {
        return SmokeSensor.find(pipe).limit(limit).skip(skip).sort(sort)
    }
    const getSmokeSensorNoPaging = (pipe) => {
        return SmokeSensor.find(pipe)
    }
    const removeSmokeSensor = (pipe) => {
        return SmokeSensor.deleteMany(pipe)
    }
    return {
        getSmokeSensorNoPaging,
        removeSmokeSensor,
        addSmokeSensor,
        getSmokeSensorAgg,
        getSmokeSensorById,
        deleteSmokeSensor,
        updateSmokeSensor,
        checkIdExist,
        getCount,
        getSmokeSensor
    }
}