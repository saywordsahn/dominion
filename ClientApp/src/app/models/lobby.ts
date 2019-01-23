import {User} from './user';

export class Lobby {
    lobbyId: number;
    name: string;
    hostId: string;
    users: User[];
    lobbyUser: LobbyUser[];

    constructor(name: string, host: User)
    {
        this.name = name;
        this.hostId = host.userId;
    }
}

export class LobbyUser {
    id: number;
    lobbyId: number;
    userId: string;
}