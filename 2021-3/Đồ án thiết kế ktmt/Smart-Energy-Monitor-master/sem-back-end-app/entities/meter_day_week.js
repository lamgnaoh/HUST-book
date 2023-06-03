const mongoose = require('mongoose');

mongoose.Promise = global.Promise;

/**
 * @swagger
 * components:
 *  schemas:
 *      MeterDayWeek:
 *          type: object
 *          properties:
 *              _id:
 *                  type: objectId
 *              deviceId:
 *                  type: objectId
 *              dateTime:
 *                  type: date
 *              activePower:
 *                  type: number
 *      
 */
const meterDayWeekSchema = new mongoose.Schema({
    _id: {
        type: mongoose.Schema.Types.ObjectId,
        required: true,
        auto: true,
    },
    deviceId: { type: mongoose.Schema.Types.ObjectId, ref: "Device" },
    datetime: { type: Date },
    activePower: { type: Number }
});

module.exports = MeterDayWeek = mongoose.model('MeterDayWeek', meterDayWeekSchema);