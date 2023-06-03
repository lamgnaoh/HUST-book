const DEFAULT_GOOGLE_APPLICATION_CREDENTIALS = require.resolve('./iotcommunication-7622e-firebase-adminsdk-3jn4o-710a72157d.json')

const serverSettings = {
    port: process.env.PORT || 3333,
    basePath: process.env.BASE_PATH || ''
}

const httpCode = {
    SUCCESS: 200,
    CREATED: 201,
    BAD_REQUEST: 400,
    TOKEN_EXPIRED: 409,
    UNKNOWN_ERROR: 520,
    FORBIDDEN: 403,
    ADMIN_REQUIRE: 406
}
const serverMqttSettings = {
    port: process.env.PORTMQTT || 8007,
    webPort: process.env.WEBPORTMQTT || 1883,
    basePath: process.env.BASE_PATH || '',
    statusTopic: process.env.STATUS_TOPIC || 'status'
}
const dbSettings = {
    db: process.env.DB || 'iot-auth',
    user: process.env.DB_USER || 'iot',
    pass: process.env.DB_PASS || 'f217s18AhSd',
    repl: process.env.DB_REPLS || '',
    servers: (process.env.DB_SERVERS) ? process.env.DB_SERVERS.split(',') : [
        '27.72.98.181:3316'
    ]
}
const rabbitConfig = {
    host: process.env.RABBIT_HOST || '27.72.30.241',
    port: process.env.RABBIT_PORT || 5672,
    user: process.env.RABBIT_USER || 'timnha24h',
    pass: process.env.RABBIT_PASS || 'timnha24h132'
}
const workerConfig = {
    queueName: process.env.QUEUE_NAME || 'iot:device',
    exchange: process.env.EXCHANGE || 'device',
}
const firebaseConfig = {
    databaseURL: process.env.FIREBASE_DATABASE_URL || 'https://vtvfun-467b4.firebaseio.com/',
    serviceAccountPath: process.env.GOOGLE_APPLICATION_CREDENTIALS || DEFAULT_GOOGLE_APPLICATION_CREDENTIALS
}

const redisConfig = {
    sentinel: process.env.REDIS_SENTINEL || '',
    clusterName: process.env.REDIS_CLUSTER_NAME || 'mymaster',
    clusterPassword: process.env.REDIS_CLUSTER_PASSWORD || 'QDCJM446bJ4K',
    db: process.env.REDIS_DB || 5,
    port: process.env.REDIS_PORT || 6379, // Redis port
    host: process.env.REDIS_HOST || '27.72.30.241', // Redis host
    expire: +process.env.EXPIRE_CACHE_SECOND || 5,
    expireSearch: process.env.EXPIRE_SEARCH || 24 * 7 * 60 * 60
}
const eventConfig = {
    EMPLOYEE_UPDATE: 'EMPLOYEE_UPDATE',
    ADD_JOB: 'ADD_JOB',
    STOP_JOB: 'STOP_JOB',
    UPDATE_CACHE: 'UPDATE_CACHE'
}
const urlConfig = {
    customer: process.env.CUSTOMER_URL || 'http://127.0.0.1:8003',
    appserver: process.env.APP_SERVER_URL || 'http://127.0.0.1:8001'
}
const serverHelper = function () {
    const jwt = require('jsonwebtoken')
    const crypto = require('crypto')
    const secretKey = process.env.SECRET_KEY || 'cur$$123$$jl1haG'

    function decodeToken(token) {
        return jwt.decode(token)
    }

    function genToken(obj) {
        return jwt.sign(obj, secretKey, {expiresIn: '1d'})
    }

    function verifyToken(token) {
        return new Promise((resolve, reject) => {
            jwt.verify(token, secretKey, (err, decoded) => {
                err ? reject(new Error(err)) : resolve(decoded)
            })
        })
    }

    function encryptPassword(password) {
        return crypto.createHash('sha256').update(password, 'binary').digest('base64')
    }

    return {decodeToken, encryptPassword, verifyToken, genToken}
}
module.exports = {
    dbSettings,
    serverHelper: serverHelper(),
    serverSettings,
    httpCode,
    serverMqttSettings,
    rabbitConfig,
    workerConfig,
    firebaseConfig,
    redisConfig,
    eventConfig,
    urlConfig
}
