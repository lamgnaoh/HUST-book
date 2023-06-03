module.exports = (container) => {
    const logger = container.resolve('logger')
    const ObjectId = container.resolve('ObjectId')
    const {
        schemaValidator,
        schemas: {
            Camera
        }
    } = container.resolve('models')
    const { httpCode, serverHelper } = container.resolve('config')
    const { cameraRepo } = container.resolve('repo')
    const addCamera = async (req, res) => {
        try {
            const body = req.body
            const {
                error,
                value
            } = await schemaValidator(body, 'Camera')
            if (error) {
                return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
            }
            const sp = await cameraRepo.addCamera(value)
            res.status(httpCode.CREATED).json(sp)
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).json({ msg: 'UNKNOWN ERROR' })
        }
    }
    const deleteCamera = async (req, res) => {
        try {
            const { id } = req.params
            if (id && id.length === 24) {
                await cameraRepo.deleteCamera(id)
                return res.status(httpCode.SUCCESS).json({ ok: true })
            }
            return res.status(httpCode.BAD_REQUEST).json()
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
        }
    }
    const getCameraById = async (req, res) => {
        try {
            const { id } = req.params
            if (id && id.length === 24) {
                const item = await cameraRepo.getCameraById(id)
                return res.status(httpCode.SUCCESS).json(item)
            }
            return res.status(httpCode.BAD_REQUEST).json({ msg: 'BAD REQUEST' })
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
        }
    }
    const updateCamera = async (req, res) => {
        try {
            const { id } = req.params
            const body = req.body
            const {
                error,
                value
            } = await schemaValidator(body, 'Camera')
            if (error) {
                return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
            }
            if (id && id.length === 24 && body) {
                const sp = await cameraRepo.updateCamera(id, value)
                return res.status(httpCode.SUCCESS).json(sp)
            }
            return res.status(httpCode.BAD_REQUEST).json()
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
        }
    }
    const getCamera = async (req, res) => {
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
                const pathType = (Camera.schema.path(i) || {}).instance || ''
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
            const data = await cameraRepo.getCamera(pipe, perPage, skip, sort)
            const total = await cameraRepo.getCount(pipe)
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
        addCamera,
        getCamera,
        getCameraById,
        updateCamera,
        deleteCamera
    }
}