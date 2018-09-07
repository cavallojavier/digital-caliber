export interface AccountUser {
    id: string;
    email: string;
    formattedName: string;
    firstName: string;
    lastName: string;
}

export interface AccountUpdate {
    email: string;
    firstName: string;
    lastName: string;
}

export interface PasswordUpdate {
    currentPassword: string;
    newPassword: string;
}