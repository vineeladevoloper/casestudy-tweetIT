import { Routes } from '@angular/router';
import { RegisterComponent } from './Pages/User/register/register.component';
import { ListUserComponent } from './Pages/User/list-user/list-user.component';
import { LoginComponent } from './Pages/User/login/login.component';
import { EditUserComponent } from './Pages/User/edit-user/edit-user.component';
import { AdminDashboardComponent } from './Pages/User/admin-dashboard/admin-dashboard.component';
import { AboutComponent } from './Pages/about/about.component';
import { ContactComponent } from './Pages/contact/contact.component';
import { UserDashboardComponent } from './Pages/User/user-dashboard/user-dashboard.component';
import { ProfileComponent } from './Pages/User/profile/profile.component';
import { UploadComponent } from './upload/upload.component';
import { AddPostComponent } from './Pages/Post/add-post/add-post.component';
import { AllPostsComponent } from './Pages/Post/all-posts/all-posts.component';

export const routes: Routes = [
    { path: 'about', component: AboutComponent },
    { path: 'contact', component: ContactComponent },
    {path:'register',component:RegisterComponent},
    {path:'login',component:LoginComponent},
    {path:'upload',component:UploadComponent},
    {path:'add-post',component:AddPostComponent},
    {path:'all-posts',component:AllPostsComponent},
    {path:'user-dashboard',component:UserDashboardComponent,
    children: [
      {path:'edit-user/:uid',component:EditUserComponent},
      {path:'profile/:uid',component:ProfileComponent},
      {path:'edit-user/:uid',component:EditUserComponent},
    ]
    },
    {path:'admin-dashboard',component:AdminDashboardComponent,
    children: [
        { path:'list-users',component:ListUserComponent },
        {path:'edit-user/:uid',component:EditUserComponent},
        {path:'profile/:uid',component:ProfileComponent},
      ]
    }
];
