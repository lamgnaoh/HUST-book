module.exports = (joi, mongoose, { joi2MongoSchema, schemas }) => {
  const ObjectId = mongoose.Types.ObjectId
  const warningJoi = joi.object({
    user: joi.string().required(),
    device: joi.string(),
    seen: joi.number().valid(0, 1).default(0),
    content: joi.string().required()
  })
  const warningSchema = joi2MongoSchema(warningJoi, {
    device: {
      type: ObjectId,
      ref: 'Device'
    }
  }, {
    createdAt: {
      type: Number,
      default: () => Math.floor(Date.now() / 1000)
    }
  })
  warningSchema.statics.validateObj = (obj, config = {}) => {
    return warningJoi.validate(obj, config)
  }
  warningSchema.statics.validateTaiLieu = (obj, config = {
    allowUnknown: true,
    stripUnknown: true
  }) => {
    return warningJoi.validate(obj, config)
  }
  const warningModel = mongoose.model('warning', warningSchema)
  warningModel.syncIndexes()
  return warningModel
}