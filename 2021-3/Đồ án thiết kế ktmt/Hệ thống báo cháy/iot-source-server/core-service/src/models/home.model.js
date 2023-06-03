module.exports = (joi, mongoose, { joi2MongoSchema, schemas }) => {
    const ObjectId = mongoose.Types.ObjectId
    const homeJoi = joi.object({
        name: joi.string().required(),
        address: joi.string().required(),
        area: joi.number().required(),
        rooms: joi.number().min(0).required(),
        floor: joi.number().min(0).required(),
        user: joi.string().required(),
        images: joi.array().items(joi.string()),
        thumbnail: joi.string().required(),
        members: joi.number().required()
    })
    const homeSchema = joi2MongoSchema(homeJoi, {
        user: {
            type: ObjectId
        }
    }, {
        createdAt: {
            type: Number,
            default: () => Math.floor(Date.now() / 1000)
        }
    })
    homeSchema.statics.validateObj = (obj, config = {}) => {
        return homeJoi.validate(obj, config)
    }
    homeSchema.statics.validateTaiLieu = (obj, config = {
        allowUnknown: true,
        stripUnknown: true
    }) => {
        return homeJoi.validate(obj, config)
    }
    const homeModel = mongoose.model('Home', homeSchema)
    homeModel.syncIndexes()
    return homeModel
}