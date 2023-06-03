module.exports = (joi, mongoose, {
    joi2MongoSchema,
    schemas
}) => {
    const roomTypes = {
        LIVING_ROOM: 1,
        BEDROOM: 2,
        KITCHEN: 3,
        DINING_ROOM: 4,
        BATHROOM: 5
    }
    const ObjectId = mongoose.Types
    const roomJoi = joi.object({
        type: joi.number().valid(...Object.values(roomTypes)),
        home: joi.string().required(),
        floor: joi.number().min(1).required(),
        area: joi.number().required(),
        position: joi.string().required(),
        owner: joi.string(),
        thumbnail: joi.string().required()
    })
    const roomSchema = joi2MongoSchema(roomJoi, {
        home: {
            type: ObjectId,
            ref: 'Home'
        }
    }, {
        createAt: {
            type: Number,
            default: () => Math.floor(Date.now() / 1000)
        }
    })
    roomSchema.statics.validateObj = (obj, config = {}) => {
        return roomJoi.validate(obj, config)
    }
    roomSchema.statics.validateDocument = (obj, config = {
        allowUnknown: true,
        stripUnknown: true
    }) => {
        return roomJoi.validate(obj, config)
    }
    const roomModel = mongoose.model('Room', roomSchema)
    roomModel.syncIndexes()
    return roomModel
}