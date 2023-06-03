const ms = require('ms')
// redis for get conversation
module.exports = container => {
  const redis = container.resolve('redis')
  const prefix = process.env.PREFIX_REDIS || 'jungotv'
  const exprire = process.env.EXPIRE_DEFAULT || '1d'
  const addPrefix = (key) => {
    return `${prefix}-${key}`
  }
  const get = (key) => {
    return redis.get(addPrefix(key))
  }
  const set = (key, value, exp = exprire) => {
    return redis.set(addPrefix(key), value, 'EX', ms(exp))
  }
  const del = (key) => {
    return redis.del(addPrefix(key))
  }
  const hset = (hkey, key, value) => {
    return redis.hset(addPrefix(hkey), key, value)
  }
  const hget = (hkey, key) => {
    return redis.hget(addPrefix(hkey), key)
  }
  const hdel = (hkey, key) => {
    return redis.hdel(addPrefix(hkey), key)
  }
  const hkeys = hkey => {
    return redis.hkeys(addPrefix(hkey))
  }
  const hlen = hkey => {
    return redis.hlen(addPrefix(hkey))
  }
  const hincrby = (hkey, key, val = 1) => {
    return redis.hincrby(addPrefix(hkey), key, val)
  }
  const hgetall = (hkey) => {
    return redis.hgetall(addPrefix(hkey))
  }
  return { get, set, hget, hset, hkeys, hlen, hdel, hincrby, hgetall, del }
}
