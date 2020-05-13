import { Injectable } from '@angular/core';
 
@Injectable()
export class ConfigService {    

    constructor() {}

    get authApiURI() {
        return 'https://eridentityserver.azurewebsites.net/api';
        // return 'http://localhost:5000/api';
    }    
     
    get resourceApiURI() {
        return 'https://erbooksonlineapi.azurewebsites.net/api';
        // return 'http://localhost:5050/api';
    }  
}