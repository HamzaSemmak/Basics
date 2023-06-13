/*
*   Response Message
*/
import { ResponseCode as Code } from "./ResponseCode";

export class Response {
    /*
    |--------------------------------------------------------------------------
    | Authentication 
    | 8000, 8001
    |--------------------------------------------------------------------------
    |
    | The following language lines are used during authentication for various
    | messages that we need to display to the user. You are free to modify
    | these language lines according to your application's requirements.
    |
    */
   
    static readonly RESPONSE_MSG_AUTH_FAILED: string = `Error : ${Code.RESPONSE_CODE_AUTH_FAILED}, These credentials do not match our records.`;
    static readonly RESPONSE_MSG_AUTH_PASSWORD: string = `Error : ${Code.RESPONSE_CODE_AUTH_PASSWORD}, The provided password is incorrect.`;
    static readonly RESPONSE_MSG_AUTH_CONNEXION: string = `Error : ${Code.RESPONSE_CODE_AUTH_CONNEXION}, Aucune connexion n’a pu être établie.`;

    /*
    |--------------------------------------------------------------------------
    | Validation Language Lines
    |--------------------------------------------------------------------------
    |
    | The following language lines contain the default error messages used by
    | the validator class. Some of these rules have multiple versions such
    | as the size rules. Feel free to tweak each of these messages here.
    |
    */

    static readonly RESPONSE_MSG_VAILDATION_FORM: string = `Error : ${Code.RESPONSE_CODE_AUTH_PASSWORD}, The Form is invalid, Please Try again.`;
    static readonly RESPONSE_MSG_VAILDATION_EMPTY: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_EMPTY}, The Form is invalid, Please Try again.`;
    static readonly RESPONSE_MSG_VAILDATION_EMAIL: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_EMAIL}, The attribute must be a valid email address.`;
}