const { required } = require('joi')
module.exports = (container) => {
    const logger = container.resolve('logger')
    const ObjectId = container.resolve('ObjectId')
    const {
        schemaValidator,
        schemas: {
            Home
        }
    } = container.resolve('models')
    const { httpCode, serverHelper } = container.resolve('config')
    const { homeRepo } = container.resolve('repo')
    const addHome = async (req, res) => {
        try {
            const { _id } = req.user
            const body = req.body
            body.user = _id.toString()
            const {
                error,
                value
            } = await schemaValidator(body, 'Home')
            if (error) {
                return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
            }
            const home = await homeRepo.addHome(value)
            res.status(httpCode.CREATED).json(home)
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).json({ msg: 'UNKNOWN ERROR' })
        }
    }
    const deleteHome = async (req, res) => {
        try {
            const { _id }= req.user
            const { id } = req.params
            if (id && id.length === 24) {
                const item = await homeRepo.getHomeById(id)
                if (item && item.user.toString() === _id.toString()) {
                    await homeRepo.deleteHome(id)
                    return res.status(httpCode.SUCCESS).json({ msg: 'Xóa thành công!' })
                }
            }
            return res.status(httpCode.BAD_REQUEST).json({ msg: 'BAD REQUEST' })
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
        }
    }
    const getHomeById = async (req, res) => {
        try {
            const { id } = req.params
            if (id && id.length === 24) {
                const item = await homeRepo.getHomeById(id)
                return res.status(httpCode.SUCCESS).json(item)
            }
            return res.status(httpCode.BAD_REQUEST).json({ msg: 'BAD REQUEST' })
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
        }
    }
    const updateHome = async (req, res) => {
        try {
            const { _id } = req.user
            const { id } = req.params
            const body = req.body

            if (id && id.length === 24) {
                const item = await homeRepo.getHomeById(id)
                if (item && item.user.toString() === _id.toString()) {
                    const {
                        error,
                        value
                    } = await schemaValidator(body, 'Home')
                    if (error) {
                        return res.status(httpCode.BAD_REQUEST).json({ msg: error.message })
                    }
                    await homeRepo.updateHome(id, value)
                    return res.status(httpCode.SUCCESS).json({ ok: true })
                }
            }
            return res.status(httpCode.BAD_REQUEST).json({ msg: 'BAD REQUEST' })
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
        }
    }
    const getHome = async (req, res) => {
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
                const vl = search[i]
                const pathType = (Home.schema.path(i) || {}).instance || ''
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
            pipe.user = _id.toString()
            const data = await homeRepo.getHome(pipe, perPage, skip, sort)
            const total = await homeRepo.getCount(pipe)
            return res.status(httpCode.SUCCESS).json({
                data,
                perPage,
                sort,
                total,
                page
            })
        } catch (e) {
            logger.e(e)
            res.status(httpCode.UNKNOWN_ERROR).send({ ok: false })
        }
    }
    return {
        addHome,
        getHome,
        getHomeById,
        updateHome,
        deleteHome
    }
}