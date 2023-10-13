export class ResponseCode {
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

    static readonly RESPONSE_CODE_AUTH_FAILED: number = 8000;
    static readonly RESPONSE_CODE_AUTH_PASSWORD: number = 8001;
    static readonly RESPONSE_CODE_AUTH_CONNEXION: number = 8002;
    static readonly RESPONSE_CODE_AUTH_EMAIL_INCORRECT: number = 8003;
    static readonly RESPONSE_CODE_AUTH_CODE_RESPONSE_INCORRECT: number = 8004;
    static readonly RESPONSE_CODE_AUTH_PASSWORD_MATCH: number = 8005;

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

    static readonly RESPONSE_CODE_VAILDATION_FORM: number = 9000;
    static readonly RESPONSE_CODE_VAILDATION_EMPTY: number = 9001;
    static readonly RESPONSE_CODE_VAILDATION_EMAIL: number = 9002;
    static readonly RESPONSE_CODE_VAILDATION_REQUIRED: number = 9003;
    static readonly RESPONSE_CODE_VAILDATION_NUMERIC: number = 9004;
    static readonly RESPONSE_CODE_VAILDATION_NUMERIC_MIN: number = 9005;
    static readonly RESPONSE_CODE_VAILDATION_NUMERIC_MAX: number = 9006;

}