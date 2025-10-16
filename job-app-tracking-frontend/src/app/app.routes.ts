import { Routes } from '@angular/router';
import { HomepageComponent } from './components/homepage/homepage.component';
import { HomepageContentComponent } from './components/homepage/homepage-content/homepage-content.component';
import { AboutComponent } from './components/about/about.component';
import { FeaturesComponent } from './components/features/features.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { LogoutComponent } from './components/logout/logout.component';
import { UserComponent } from './user/user.component';
import { AnalyticsComponent } from './user/analytics/analytics.component';
import { DashboardComponent } from './user/dashboard/dashboard.component';
import { JobsListComponent } from './user/jobs-list/jobs-list.component';
import { ProfileComponent } from './user/profile/profile.component';
import { AdminComponent } from './admin/admin.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AdminMetricsComponent } from './admin/admin-metrics/admin-metrics.component';
import { AdminSearchUsersComponent } from './admin/admin-search-users/admin-search-users.component';



export const routes: Routes = [
    {
        path: '',
        component: HomepageComponent,
        children:[
            {path: '', title: 'Home Page', component: HomepageContentComponent},
            {path: 'features', title: 'Features', component: FeaturesComponent},
            {path: 'about', title: 'About', component: AboutComponent},
            {path: 'login', title: 'Login', component: LoginComponent},
            {path: 'signup', title: 'Signup', component: SignupComponent},
            {path: 'login', title: 'Login', component: LoginComponent},
            {path: 'logout', title: 'Logout', component: LogoutComponent}
        ]
    },
    {
        path: 'user',
        component: UserComponent,
        children:[
            {path: 'user', title: ' Dashboard', component: DashboardComponent},
            {path: 'analytics', title: 'Analytics', component: AnalyticsComponent},
            {path: 'job-list', title: 'Jobs List', component: JobsListComponent},
            {path: 'profile', title: 'Profile', component: ProfileComponent}
        ]
    },
    {
        path: 'admin',
        component: AdminComponent,
        children:[
            {path: 'admin', title: ' Dashboard', component: AdminDashboardComponent},
            {path: 'admin-metrics', title: ' Metrics', component: AdminMetricsComponent},
            {path: 'admin-search', title: 'Search Users', component: AdminSearchUsersComponent}
        ]
    }

];
