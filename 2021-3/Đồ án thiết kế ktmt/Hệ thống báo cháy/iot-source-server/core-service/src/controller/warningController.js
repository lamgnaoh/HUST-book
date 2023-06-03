module.exports = (container) => {
    const logger = container.resolve('logger')
    const ObjectId = container.resolve('ObjectId')
    const {
        schemaValidator,
        schemas: {
            Warning
        }
    } = container.resolve('models')
    const { httpCode, serverHelper } = container.resolve('config')
    const { warningRepo } = container.resolve('repo')
    const addWarning = async (req, res) => {
        try {
            const body = req.body
            const {
                error,
                value
            } = await schemaValidator(body, 'Warning')
            if (error) {
                return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
            }
            const item = await warningRepo.addWarning(value)
            return res.status(httpCode.CREATED).json(item)
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).json({ msg: 'UNKNOWN ERROR', ok: false })
        }
    }
    const deleteWarning = async (req, res) => {
        try {
            const { id } = req.params
            if (id && id.length === 24) {
                await warningRepo.deleteWarning(id)
                return res.status(httpCode.SUCCESS).json({ ok: true })
            }
            return res.status(httpCode.BAD_REQUEST).json({ msg: 'BAD REQUEST', ok: false })
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).json({ ok: false })
        }
    }
    const getWarningById = async (req, res) => {
        try {
            const { id } = req.params
            const { _id } = req.user
            if (id && id.length === 24) {
                const item = await warningRepo.getWarningById(id)
                if (item && item.user.toString() === _id.toString()) {
                    await warningRepo.updateWarning(id, { seen: 1 })
                    return res.status(httpCode.SUCCESS).json(item)
                }
            }
            return res.status(httpCode.BAD_REQUEST).json({ msg: 'BAD REQUEST', ok: false })
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).json({ msg: 'UNKNOWN ERROR', ok: false })
        }
    }
    const updateWarning = async (req, res) => {
        try {
            const { id } = req.params
            const body = req.body
            if (id && id.length === 24 && body) {
                const {
                    error,
                    value
                } = await schemaValidator(body, 'Warning')
                if (error) {
                    return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
                }
                const item = await warningRepo.updateWarning(id, value)
                return res.status(httpCode.SUCCESS).json(item)
            }
            return res.status(httpCode.BAD_REQUEST).json()
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).json({ ok: false })
        }
    }
    const getWarning = async (req, res) => {
        try {
            const { _id } = req.user
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
                const value = search[i]
                const pathType = (Warning.schema.path(i) || {}).instance || ''
                if (pathType.toLowerCase() === 'objectid') {
                    pipe[i] = value ? ObjectId(value) : { $exists: false }
                } else if (pathType === 'Number') {
                    pipe[i] = +value ? +value : 0
                } else if (pathType === 'String' && value.constructor === String) {
                    pipe[i] = new RegExp(value.replace(/\\/g, '\\\\'), 'gi')
                } else {
                    pipe[i] = value
                }
            })
            // pipe.user = _id.toString()
            const data = await warningRepo.getWarning(pipe, perPage, skip, sort)
            const total = await warningRepo.getCount(pipe)
            return res.status(httpCode.SUCCESS).json({
                data,
                perPage,
                skip,
                sort,
                total,
                page
            })
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).json({ ok: false, msg: 'UNKNOWN ERROR' })
        }
    }
    return {
        addWarning,
        getWarning,
        getWarningById,
        updateWarning,
        deleteWarning
    }
}