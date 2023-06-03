import BaseApi from "../base/BaseApi.js"
import BaseApiConfig from "../base/BaseApiConfig.js"

class AuthApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "Auth";
    }

    login(email, password){
        return BaseApiConfig.post(`${this.apiController}/login`, {email,password});
    }
    getInfo(){
        return BaseApiConfig.get(`${this.apiController}/user-info`,{
            headers: {
                "Content-Type": "application/json",
                Authorization: sessionStorage.getItem("token"),
            }
        });
    }
}
export default new AuthApi();