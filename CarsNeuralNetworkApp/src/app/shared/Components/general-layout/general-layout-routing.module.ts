import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeneralLayoutComponent } from './general-layout-home/general-layout.component';

const routes: Routes = [
  {
    path: '',
    component: GeneralLayoutComponent,
    // Order of children routes is important.
    // We should order it from most specific to less specific
    children: [
      {
        path: '',
        loadChildren: () =>
          import('src/app/pages/home/home.module').then((m) => m.HomeModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GeneralLayoutRoutingModule {}
