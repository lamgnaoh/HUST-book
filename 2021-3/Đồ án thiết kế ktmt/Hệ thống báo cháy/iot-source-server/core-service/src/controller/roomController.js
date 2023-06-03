module.exports = (container) => {
  const logger = container.resolve('logger')
  const ObjectId = container.resolve('ObjectId')
  const {
    schemaValidator,
    schemas: {
      Room
    }
  } = container.resolve('models')
  const { httpCode, serverHelper } = container.resolve('config')
  const { roomRepo, homeRepo } = container.resolve('repo')
  const addRoom = async (req, res) => {
    try {
      const body = req.body
      const {
        error,
        value
      } = await schemaValidator(body, 'Room')
      if (error) {
        return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
      }
      const sp = await roomRepo.addRoom(value)
      res.status(httpCode.CREATED).json(sp)
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).json({ msg: 'UNKNOWN ERROR' })
    }
  }
  const deleteRoom = async (req, res) => {
    try {
      const { id } = req.params
      if (id && id.length === 24) {
        await roomRepo.deleteRoom(id)
        return res.status(httpCode.SUCCESS).json({ ok: true })
      }
      return res.status(httpCode.BAD_REQUEST).json()
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
    }
  }
  const getRoomById = async (req, res) => {
    try {
      const { id } = req.params
      if (id && id.length === 24) {
        const item = await roomRepo.getRoomById(id)
        return res.status(httpCode.SUCCESS).json(item)
      }
      return res.status(httpCode.BAD_REQUEST).json({ msg: 'BAD REQUEST' })
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
    }
  }
  const updateRoom = async (req, res) => {
    try {
      const { id } = req.params
      const body = req.body
      const {
        error,
        value
      } = await schemaValidator(body, 'Room')
      if (error) {
        return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
      }
      if (id && id.length === 24 && body) {
        const sp = await roomRepo.updateRoom(id, value)
        return res.status(httpCode.SUCCESS).json(sp)
      }
      return res.status(httpCode.BAD_REQUEST).json()
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
    }
  }
  const getRoom = async (req, res) => {
    try {
      let {
        page,
        perPage,
        sort,
        ids,
        home
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
      delete search.home

      Object.keys(search).forEach(i => {
        const vl = search[i]
        const pathType = (Room.schema.path(i) || {}).instance || ''
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
      if (home && home.length === 24) {
        pipe.home = home
        const data = await roomRepo.getRoom(pipe, perPage, skip, sort)
        const total = await roomRepo.getCount(pipe)
        return res.status(httpCode.SUCCESS).json({
          data,
          perPage,
          sort,
          total,
          page
        })
      }
      return res.status(httpCode.BAD_REQUEST).json({ msg: 'Home is required! ' })
    } catch (e) {
      logger.e(e)
      res.status(httpCode.UNKNOWN_ERROR).json({ ok: false, msg: 'UNKNOWN ERROR' })
    }
  }
  return {
    addRoom,
    getRoom,
    getRoomById,
    updateRoom,
    deleteRoom
  }
}