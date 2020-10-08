export class Joke {
    id: number;
    jokerID: number;
    jokerName:string;
    withBadge:boolean;
    title: string;
    description: string;

    constructor(id: number,jokerID:number,jokerName:string,withBadge:boolean,title:string,description:string) {
        this.id=id;
        this.jokerID=jokerID;
        this.jokerName=jokerName;
        this.withBadge=withBadge;
        this.title=title;
        this.description=description;
    }
}