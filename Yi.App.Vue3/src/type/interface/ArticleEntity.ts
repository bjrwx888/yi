export interface ArticleEntity{
    title: string;
    content: string;
    images:string[];
    isDeleted:boolean;
    creationTime:string;
}
// import { ArticleEntity } from '@/type/interface/ArticleEntity'