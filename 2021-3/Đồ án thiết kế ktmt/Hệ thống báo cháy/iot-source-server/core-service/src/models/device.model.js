module.exports = (joi, mongoose, { joi2MongoSchema, schemas }) => {
    const ObjectId = mongoose.Types.ObjectId
    const typeDevices = {
        FLAME_SENSOR: 1,
        GAS_SENSOR: 2
    }
    const deviceJoi = joi.object({
        name: joi.string().required(),
        room: joi.string().required(),
        type: joi.number().valid(...Object.values(typeDevices)).required(),
        position: joi.string().required(),
        thumbnail: joi.string().required(),
        producer: joi.string().required()
    })
    const deviceSchema = joi2MongoSchema(deviceJoi, {
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
    deviceSchema.statics.validateObj = (obj, config = {}) => {
        return deviceJoi.validate(obj, config)
    }
    deviceSchema.statics.validateTaiLieu = (obj, config = {
        allowUnknown: true,
        stripUnknown: true
    }) => {
        return deviceJoi.validate(obj, config)
    }
    const deviceModel = mongoose.model('Device', deviceSchema)
    deviceModel.syncIndexes()
    return deviceModel
}