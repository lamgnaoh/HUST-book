module.exports = (joi, mongoose, { joi2MongoSchema, schemas }) => {
    const ObjectId = mongoose.Types.ObjectId
    const cameraJoi = joi.object({
        name: joi.string().required(),
        position: joi.string().required(),
        room: joi.string().required()
    })
    const cameraSchema = joi2MongoSchema(cameraJoi, {
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
    cameraSchema.statics.validateObj = (obj, config = {}) => {
        return cameraJoi.validate(obj, config)
    }
    cameraSchema.statics.validateTaiLieu = (obj, config = {
        allowUnknown: true,
        stripUnknown: true
    }) => {
        return cameraJoi.validate(obj, config)
    }
    const cameraModel = mongoose.model('Camera', cameraSchema)
    cameraModel.syncIndexes()
    return cameraModel
}