module.exports = container => {
  const { schemas } = container.resolve('models')
  const { Home } = schemas
  const addHome = (data) => {
    const n = new Home(data)
    return n.save()
  }
  const getHomeById = (id) => {
    return Home.findById(id)
  }
  const deleteHome = (id) => {
    return Home.findByIdAndRemove(id, { useFindAndModify: false })
  }
  const updateHome = (id, n) => {
    return Home.findByIdAndUpdate(id, n, {
      useFindAndModify: false,
      returnOriginal: false
    })
  }
  const checkIdExist = (id) => {
    return Home.findOne({ id })
  }
  const getCount = (pipe = {}) => {
    return Home.countDocuments(pipe)
  }
  const getHomeAgg = (pipe) => {
    return Home.aggregate(pipe)
  }
  const getHome = (pipe, limit, skip, sort) => {
    return Home.find(pipe).limit(limit).skip(skip).sort(sort)
  }
  const getHomeNoPaging = (pipe) => {
    return Home.find(pipe)
  }
  const removeHome = (pipe) => {
    return Home.deleteMany(pipe)
  }
  return {
    getHomeNoPaging,
    removeHome,
    addHome,
    getHomeAgg,
    getHomeById,
    deleteHome,
    updateHome,
    checkIdExist,
    getCount,
    getHome
  }
}