import BaseApi from "../base/BaseApi.js"
import BaseApiConfig from "../base/BaseApiConfig.js"

class AssignmentApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "Assignment";
    }
    getAssignmentByClassId(id) {
        return BaseApiConfig.get(`${this.apiController}/Class/${id}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
    postAssignmentByClassId(id, body) {
        return BaseApiConfig.post(`${this.apiController}?classId=${id}`, body, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
    submit(id, files) {
        return BaseApiConfig.post(`${this.apiController}/${id}/submit`, files, {
            headers: {
                "Content-Type": "multipart/form-data",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
}

export default new AssignmentApi();