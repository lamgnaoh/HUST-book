module.exports = (joi, mongoose, { joi2MongoSchema, schemas }) => {
    const ObjectId = mongoose.Types.ObjectId
    const smokeSensorJoi = joi.object({
        code: joi.string().required(),
        name: joi.string().required(),
        room: joi.string().required(),
        thumbnail: joi.string().required(),
        active: joi.number().valid(0, 1)
    })
    const smokeSensorSchema = joi2MongoSchema(smokeSensorJoi, {
        room: {
            type: ObjectId,
            ref: 'Room'
        }
    }, {
        createdAt: {
            type: Number,
            default: () => Math.floor(Date.now() / 1000)
        }
    })
    smokeSensorSchema.statics.validateObj = (obj, config = {}) => {
        return smokeSensorJoi.validate(obj, config)
    }
    smokeSensorSchema.statics.validateTaiLieu = (obj, config = {
        allowUnknown: true,
        stripUnknown: true
    }) => {
        return smokeSensorJoi.validate(obj, config)
    }
    const smokeSensorModel = mongoose.model('smokeSensor', smokeSensorSchema)
    smokeSensorModel.syncIndexes()
    return smokeSensorModel
}