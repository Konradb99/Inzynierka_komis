import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CurrentCarsListService {

constructor() { }

currentList:string='main';
currentPageForMain:number=1;
currentPageForFilterd:number=1;

setCurrentList(list:string){
  this.currentList=list;
}

getCurrentList(){
  return this.currentList;
}

setCurrentPage(page:number){
  switch(this.currentList) {
    case 'main' :
      this.currentPageForMain=page+1;
      this.currentPageForFilterd=1;
      break;
    case 'filtered' : 
      this.currentPageForFilterd=page+1;
      this.currentPageForMain=1;
      break;
    case 'knn' :
      this.currentPageForFilterd=1;
      this.currentPageForMain=1;
      break;
    case 'mix' :
      this.currentPageForFilterd=1;
      this.currentPageForMain=1;
      break;
  }
}

}
