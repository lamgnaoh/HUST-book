import BaseApi from "../base/BaseApi.js"
import BaseApiConfig from "../base/BaseApiConfig.js"

class UserApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "Users";
    }
    search(paramStrs){
        return BaseApiConfig.get(`${this.apiController}/search?keyword=${paramStrs}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
    changePass(body){
        return BaseApiConfig.post(`${this.apiController}/me`, body, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
    getStudent(pageNumber,pageSize){
        return BaseApiConfig.get(`${this.apiController}/students?PageNumber=${pageNumber}&PageSize=${pageSize}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
}

export default new UserApi();