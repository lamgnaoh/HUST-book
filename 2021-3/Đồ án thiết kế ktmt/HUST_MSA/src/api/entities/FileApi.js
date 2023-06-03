import BaseApi from "../base/BaseApi.js"
import BaseApiConfig from "../base/BaseApiConfig.js"

class FileApi extends BaseApi {
    constructor() {
        super();
        this.apiController = "File";
    }

    upload(){
        return BaseApiConfig.post(`${this.apiController}/upload`);
    }
    download(fileName){
        return BaseApiConfig.get(`${this.apiController}/download?filename=${fileName}`);
    }
}
export default new FileApi();