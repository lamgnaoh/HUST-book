export default class FormatData {

    /**
     * định dang ngày về dạjng dd/mm/yyyy
     * @param {*} _date 
     * @returns 
     */
    static formatDate(_date) {
        if (_date != null) {
            var date = new Date(_date);
            var day = date.getDate();
            day = day < 10 ? "0" + day : day;
            var month = date.getMonth() + 1;
            month = month < 10 ? "0" + month : month;
            var year = date.getFullYear();
            return day + "/" + month + "/" + year;
        } else {
            return "";
        }
    }

    /**
     * format date về dạng yyyy-mm-dd
     * @param _date
     */
    static formatDateToValue(_date) {
        if (_date != null) {
            var date = new Date(_date);
            var day = date.getDate();
            day = day < 10 ? "0" + day : day;
            var month = date.getMonth() + 1;
            month = month < 10 ? "0" + month : month;
            var year = date.getFullYear();
            return year + "-" + month + "-" + day;
        } else {
            return "";
        }
    }

}