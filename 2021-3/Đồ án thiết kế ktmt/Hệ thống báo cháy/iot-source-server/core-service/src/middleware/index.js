const request = require('request-promise')
module.exports = (container) => {
  const {
    serverHelper,
    httpCode
  } = container.resolve('config')
  const logger = container.resolve('logger')
  const verifyAccessToken = async (req, res, next) => {
    // req.user = {}
    // return next()
    // retain when hacker using postman !
    try {
      const token = req.headers['x-access-token'] || ''
      if (!token) {
        return res.status(httpCode.BAD_REQUEST).json({ msg: 'Bạn không có quyền thực hiện tác vụ này.' })
      }
      const option = {
        uri: process.env.AUTHORIZATION_URL || `http://27.72.98.181:3303/authorization`,
        headers: {
          'x-access-token': token
        },
        method: 'GET',
        resolveWithFullResponse: true
      }
      const result = await request(option)
      const userAuthorization = JSON.parse(result.body)
      if (result.statusCode === httpCode.SUCCESS) {
        req.user = userAuthorization
        return next()
      } else {
        res.status(httpCode.BAD_REQUEST).json({ msg: 'Bạn không có quyền thực hiện tác vụ này.' })
      }
    } catch (e) {
      if (!e.message.includes('TokenExpiredError')) {
        logger.e(e)
      }
      logger.d(req.headers)
      res.status(httpCode.TOKEN_EXPIRED).json({})
    }
  }
  return { verifyAccessToken }
}
