module.exports = container => {
  const { schemas } = container.resolve('models')
  const { Warning } = schemas
  const addWarning = async (data) => {
    const n = new Warning(data)
    return n.save()
  }
  const getWarningById = async (id) => {
    return Warning.findById(id)
  }
  const deleteWarning = async (id) => {
    return Warning.findByIdAndRemove(id, { useFindAndModify: false })
  }
  const updateWarning = async (id, n) => {
    return Warning.findByIdAndUpdate(id, n, {
      useFindAndModify: false,
      returnOriginal: false
    })
  }
  const checkIdExist = async (id) => {
    return Warning.findOne({ id })
  }
  const getCount = async (pipe = {}) => {
    return Warning.countDocuments(pipe)
  }
  const getWarningAgg = async (pipe) => {
    return Warning.aggregate(pipe)
  }
  const getWarning = async (pipe, limit, skip, sort) => {
    return Warning.find(pipe).limit(limit).skip(skip).sort(sort)
  }
  const getWarningNoPaging = async (pipe) => {
    return Warning.find(pipe)
  }
  const removeWarning = async (pipe) => {
    return Warning.deleteMany(pipe)
  }
  return {
    getWarningNoPaging,
    removeWarning,
    addWarning,
    getWarningAgg,
    getWarningById,
    deleteWarning,
    updateWarning,
    checkIdExist,
    getCount,
    getWarning
  }
}