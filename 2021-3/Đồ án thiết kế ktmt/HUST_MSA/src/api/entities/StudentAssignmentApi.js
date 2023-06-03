import BaseApi from "../base/BaseApi.js"
import BaseApiConfig from "../base/BaseApiConfig.js"

class UserApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "StudentAssignment";
    }
    getStudentAssignment(id){
        return BaseApiConfig.get(`${this.apiController}/Assignments/${id}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
    reviewAssignment(id1,id2,body){
        return BaseApiConfig.put(`${this.apiController}/Assignments/${id1}/Student/${id2}`, body,{
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
}

export default new UserApi();