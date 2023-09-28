import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoadingComponent } from './shared/components/loading/loading.component';

const routes: Routes = [
  { path: '', component: HomeComponent, title: 'Home' },
  {
    path: 'account',
    loadChildren: () =>
      import('../app/account/account.module').then((m) => m.AccountModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
