import axios from "axios";

var BaseAPIConfig = axios.create({
    baseURL: "https://localhost:7089/",

});

export default BaseAPIConfig;