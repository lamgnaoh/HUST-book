module.exports = container => {
    const { schemas } = container.resolve('models')
    const { Room } = schemas
    const addRoom = async (data) => {
        const n = new Room(data)
        return n.save()
    }
    const getRoomById = async (id) => {
        return Room.findById(id)
    }
    const deleteRoom = async (id) => {
        return Room.findByIdAndRemove(id, { useFindAndModify: false })
    }
    const updateRoom = async (id, n) => {
        return Room.findByIdAndUpdate(id, n, {
            useFindAndModify: false,
            returnOriginal: false
        })
    }
    const checkIdExist = async (id) => {
        return Room.findOne({ id })
    }
    const getCount = async (pipe = {}) => {
        return Room.countDocuments(pipe)
    }
    const getRoomAgg = async (pipe) => {
        return Room.aggregate(pipe)
    }
    const getRoom = async (pipe, limit, skip, sort) => {
        return Room.find(pipe).limit(limit).skip(skip).sort(sort)
    }
    const getRoomNoPaging = async (pipe) => {
        return Room.find(pipe)
    }
    const removeRoom = async (pipe) => {
        return Room.deleteMany(pipe)
    }
    return {
        getRoomNoPaging,
        removeRoom,
        addRoom,
        getRoomAgg,
        getRoomById,
        deleteRoom,
        updateRoom,
        checkIdExist,
        getCount,
        getRoom
    }
}