import BaseApiConfig from "./BaseApiConfig.js"

export default class BaseApi {
    constructor() {
        this.apiController = null;
    }

    /**
     * Lấy toàn bộ
     * @returns 
     */
    getAll() {
        return BaseApiConfig.get(`${this.apiController}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }

    /**
     * post bản ghi
     * @param {*} body 
     * @returns 
     */
    add(body) {
        return BaseApiConfig.post(`${this.apiController}`, body, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }

    /**
     * Lấy bản ghi có id
     * @param {*} id 
     * @returns 
     */
    getById(id) {
        return BaseApiConfig.get(`${this.apiController}/${id}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }

    /**
     * Sử bản ghi có id
     * @param {*} id 
     * @param {*} body 
     * @returns 
     */
    update(id, body) {
        return BaseApiConfig.put(`${this.apiController}/${id}`, body, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }

    /**
     * Xóa bản ghi có id
     * @param {*} id 
     * @returns 
     */
    delete(id) {
        return BaseApiConfig.delete(`${this.apiController}/${id}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }

    /**
     * lấy mã mới
     * @returns 
     */
    getNewCode() {
        return BaseApiConfig.get(`${this.apiController}/NewCode`);
    }

    /**
     * lấy file export excel
     * */
    getExport() {
        return BaseApiConfig.get(`${this.apiController}/export`, { responseType: "blob" });
    }

    /**
     * lấy dữ liệu lọc và phân trang
     * @param {any} paramStrs
     */
    getFilterPaging(paramStrs) {
        return BaseApiConfig.get(`${this.apiController}?${paramStrs}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }

    /**
     * Xóa nhiều bản ghi
     * @param {any} ids
     */
    deleteBatch(ids) {
        return BaseApiConfig.delete(`${this.apiController}/deleteBatch`, ids);
    }
}