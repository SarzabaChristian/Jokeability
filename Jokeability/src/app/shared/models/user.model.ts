export class User {
    constructor(
        public id: number,
        public username: string,
        public firstName: string,
        public lastName: string,
        private _token: string,
        private _tokenExpirationDate: Date
    ) { }

    get fullName() {
        return this.lastName + ' ' + this.firstName;
    }

    get token() {
        if (!this._tokenExpirationDate || new Date() > this._tokenExpirationDate) {
            return null;
        }

        return this._token;
    }
}