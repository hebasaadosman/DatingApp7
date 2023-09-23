import { ResolveFn } from '@angular/router';
import { Member } from '../_models/member';
import { inject } from '@angular/core';
import { MembersService } from '../_services/members.service';

//it is a function that takes a route and returns a member
//this function will be called by the router when it needs data for a particular route
//like a guard, it will be called before the component is loaded
export const memberDetailsResolver: ResolveFn<Member> = (route, state) => {
  const memberService=inject(MembersService);
  return memberService.getMember(route.paramMap.get('username')!);
};
