module.exports = (container) => {
  const logger = container.resolve('logger')
  const ObjectId = container.resolve('ObjectId')
  const {
    schemaValidator,
    schemas: {
      SmokeSensor
    }
  } = container.resolve('models')
  const { httpCode, serverHelper } = container.resolve('config')
  const { smokeSensorRepo } = container.resolve('repo')
  const addSmokeSensor = async (req, res) => {
    try {
      const body = req.body
      const {
        error,
        value
      } = await schemaValidator(body, 'SmokeSensor')
      if (error) {
        return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
      }
      const sp = await smokeSensorRepo.addSmokeSensor(value)
      res.status(httpCode.CREATED).json(sp)
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).json({ msg: 'UNKNOWN ERROR' })
    }
  }
  const deleteSmokeSensor = async (req, res) => {
    try {
      const { id } = req.params
      if (id && id.length === 24) {
        await smokeSensorRepo.deleteSmokeSensor(id)
        return res.status(httpCode.SUCCESS).json({ ok: true })
      }
      return res.status(httpCode.BAD_REQUEST).json()
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
    }
  }
  const getSmokeSensorById = async (req, res) => {
    try {
      const { id } = req.params
      if (id && id.length === 24) {
        const item = await smokeSensorRepo.getSmokeSensorById(id)
        return res.status(httpCode.SUCCESS).json(item)
      }
      return res.status(httpCode.BAD_REQUEST).json({ msg: 'BAD REQUEST' })
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
    }
  }
  const updateSmokeSensor = async (req, res) => {
    try {
      const { id } = req.params
      const body = req.body
      const {
        error,
        value
      } = await schemaValidator(body, 'SmokeSensor')
      if (error) {
        return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
      }
      if (id && id.length === 24 && body) {
        const sp = await smokeSensorRepo.updateSmokeSensor(id, value)
        return res.status(httpCode.SUCCESS).json(sp)
      }
      return res.status(httpCode.BAD_REQUEST).json()
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
    }
  }
  const getSmokeSensor = async (req, res) => {
    try {
      let {
        page,
        perPage,
        sort,
        ids
      } = req.query
      page = +page || 1
      perPage = +perPage || 10
      sort = +sort === 0 ? { _id: 1 } : +sort || { _id: -1 }
      const skip = (page - 1) * perPage
      const search = { ...req.query }
      const pipe = {}

      if (ids) {
        if (ids.constructor === Array) {
          pipe.id = { $in: ids }
        } else if (ids.constructor === String) {
          pipe.id = { $in: ids.split(',') }
        }
      }

      delete search.ids
      delete search.page
      delete search.perPage
      delete search.sort
      Object.keys(search).forEach(i => {
        const vl = search[i]
        const pathType = (SmokeSensor.schema.path(i) || {}).instance || ''
        if (pathType.toLowerCase() === 'objectid') {
          pipe[i] = ObjectId(vl)
        } else if (pathType === 'Number') {
          pipe[i] = +vl
        } else if (pathType === 'String' && vl.constructor === String) {
          pipe[i] = new RegExp(vl, 'gi')
        } else {
          pipe[i] = vl
        }
      })
      const data = await smokeSensorRepo.getSmokeSensor(pipe, perPage, skip, sort)
      const total = await smokeSensorRepo.getCount(pipe)
      res.status(httpCode.SUCCESS).send({
        perPage,
        skip,
        sort,
        data,
        total,
        page
      })
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
    }
  }
  return {
    addSmokeSensor,
    getSmokeSensor,
    getSmokeSensorById,
    updateSmokeSensor,
    deleteSmokeSensor
  }
}