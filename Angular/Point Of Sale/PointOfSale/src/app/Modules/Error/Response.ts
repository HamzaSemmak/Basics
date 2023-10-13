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
    static readonly RESPONSE_MSG_AUTH_EMAIL_INCORRECT: string = `Error : ${Code.RESPONSE_CODE_AUTH_EMAIL_INCORRECT}, The provided email is incorrect.`;
    static readonly RESPONSE_MSG_AUTH_CODE_RESPONSE_INCORRECT: string = `Error : ${Code.RESPONSE_CODE_AUTH_CODE_RESPONSE_INCORRECT}, The provided code is incorrect.`;
    static readonly RESPONSE_MSG_AUTH_PASSWORD_MATCH: string = `Error : ${Code.RESPONSE_CODE_AUTH_PASSWORD_MATCH}, Your password and confirmation password do not match.`;

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

    static readonly RESPONSE_MSG_VAILDATION_FORM: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_FORM}, The Form is invalid, Please Try again.`;
    static readonly RESPONSE_MSG_VAILDATION_EMPTY: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_EMPTY}, The Form is invalid, Please Try again.`;
    static readonly RESPONSE_MSG_VAILDATION_EMAIL: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_EMAIL}, The attribute must be a valid email address.`;
    static readonly RESPONSE_MSG_VAILDATION_REQUIRED: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_REQUIRED}, The email attribute field is required`;
    static readonly RESPONSE_MSG_VAILDATION_NUMERIC: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_NUMERIC}, The attribute must be a number.`;
    static readonly RESPONSE_MSG_VAILDATION_NUMERIC_MIN: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_NUMERIC_MIN}, The attribute must be at least 8 charactere .`;
    static readonly RESPONSE_MSG_VAILDATION_NUMERIC_MAX: string = `Error : ${Code.RESPONSE_CODE_VAILDATION_NUMERIC_MAX}, The attribute must not be greater than 20 charactere`;
}