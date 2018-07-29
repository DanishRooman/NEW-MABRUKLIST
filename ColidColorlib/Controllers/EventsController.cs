using DataAccessLayer.DBContext;
using DataTransferObjects.Category;
using DataTransferObjects.ChooseEvent;
using DataTransferObjects.Congregation;
using DataTransferObjects.Event;
using DataTransferObjects.Group;
using DataTransferObjects.Invitation;
using DataTransferObjects.Neighbourhood;
using DataTransferObjects.Types;
using DataTransferObjects.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ColidColorlib.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEvent()
        {
            EventDTO evnt = new EventDTO();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                //Category
                evnt.eventCategoryList = dbcontext.mblist_category.AsEnumerable().Select(x => new CategoryDto
                {
                    Id = x.cat_key,
                    Category = x.cat_name
                }).ToList();

                //Types
                evnt.eventTypesList = dbcontext.mblist_type.AsEnumerable().Select(x => new TypeDto
                {

                    id = x.type_key,
                    Type = x.type_name

                }).ToList();
            };
            return PartialView("_AddEvent", evnt);
        }

        [HttpPost]
        public ActionResult AddGuests(List<EventGuests> guests)
        {
            try
            {
                if (guests.Any())
                {
                    using (MABRUKLISTEntities dbcontex = new MABRUKLISTEntities())
                    {
                        int eventKey = Convert.ToInt32(guests.FirstOrDefault().EventId);
                        var oldGuests = dbcontex.mblist_event_guests.Where(x => x.guest_event_key == eventKey).ToList();
                        foreach (var g in oldGuests)
                        {
                            dbcontex.mblist_event_guests.Remove(g);
                        }
                        foreach (var guest in guests)
                        {
                            if (guest.userId != null && guest.userId.Trim() != "" && guest.EventId != null && guest.EventId.Trim() != "" && guest.EventId != "0")
                            {
                                mblist_event_guests gst = new mblist_event_guests()
                                {
                                    guest_user_key = guest.userId,
                                    guest_event_key = Convert.ToInt32(guest.EventId)
                                };
                                dbcontex.mblist_event_guests.Add(gst);
                                dbcontex.SaveChanges();
                            }
                        }

                    };
                    return Json(new { key = true, value = "Guest Choosed Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { key = false, value = "pleas choose contact" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new { key = false, value = "Unable to choose contact" }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpGet]
        public ActionResult AddContact()
        {
            using (MABRUKLISTEntities dbcontaxt = new MABRUKLISTEntities())
            {
                //Group
                ViewBag.groups = dbcontaxt.mblist_group.AsEnumerable().Select(x => new SelectListItem
                {
                    Value = x.group_key.ToString(),
                    Text = x.group_name.ToString()
                }).ToList();

                //Congregation
                ViewBag.congregations = dbcontaxt.mblist_congregation.AsEnumerable().Select(x => new SelectListItem
                {
                    Value = x.congregation_key.ToString(),
                    Text = x.congregation_name
                }).ToList();

                //Nieghbourhood
                ViewBag.nieghbourhood = dbcontaxt.mblist_neighborhoods.AsEnumerable().Select(x => new SelectListItem
                {
                    Value = x.neighborhoods_key.ToString(),
                    Text = x.neighborhoods_name
                }).ToList();
            };
            return PartialView("_AddContact");
        }

        [HttpGet]
        public ActionResult SetInvitationCard(int eventID)
        {
            using (MABRUKLISTEntities dbcontaxt = new MABRUKLISTEntities())
            {
                var events = dbcontaxt.mblist_events_detail.Find(eventID);
                if (events != null)
                {
                    ViewBag.background = string.Empty;
                    ViewBag.EventTitle = events.event_detail_title;
                    ViewBag.DateTime = events.event_detail_date;
                    ViewBag.Address = events.event_detail_address;
                    ViewBag.type = dbcontaxt.mblist_type.Find(events.event_detail_type_key) != null ? dbcontaxt.mblist_type.Find(events.event_detail_type_key).type_name : string.Empty;
                }
            };
            return PartialView("_InvitationCard");
        }


        [HttpGet]
        public ActionResult SetInvitationTemplate(int eventID,int templateKey)
        {
            using (MABRUKLISTEntities dbcontaxt = new MABRUKLISTEntities())
            {
                var template = dbcontaxt.mblist_invitation_cards.Find(templateKey);
                var events = dbcontaxt.mblist_events_detail.Find(eventID);
                if (events != null)
                {
                    ViewBag.background = template != null ? template.invitation_img_path : string.Empty;
                    ViewBag.EventTitle = events.event_detail_title;
                    ViewBag.DateTime = events.event_detail_date;
                    ViewBag.Address = events.event_detail_address;
                    ViewBag.type = dbcontaxt.mblist_type.Find(events.event_detail_type_key) != null ? dbcontaxt.mblist_type.Find(events.event_detail_type_key).type_name : string.Empty;
                }
            };
            return PartialView("_InvitationCard");
        }

        public ActionResult UserDetails(int? group, int? Congregations, int? neighbourhood)
        {
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                var allusers = dbcontext.mblist_user_info.AsEnumerable();
                if (group != null)
                {
                    allusers = allusers.Where(x => x.usr_group_key == group);
                }
                if (Congregations != null)
                {
                    allusers = allusers.Where(x => x.usr_congregation_key == Congregations);
                }
                if (neighbourhood != null)
                {
                    allusers = allusers.Where(x => x.usr_neighbourhood_key == neighbourhood);
                }
                var users = allusers.Select(x => new ContactUsersDto
                {
                    Id = x.usr_key,
                    Users = (x.mblist_title != null ? x.mblist_title.title_name + " " : string.Empty) + (x.usr_first_name + " " + x.usr_last_name),
                    Action = "<input class='userscbox' confirmed='false' type='checkbox' value='" + x.AspNetUsers.Id + "'>"
                }).ToList();
                return Json(users, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddOrUpdateEvents(EventDTO dtoEvent)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    DateTime invitationDate = DateTime.ParseExact(dtoEvent.Date, "MM/dd/yyyy h:mm tt", null);
                    //Following line is used to get logged in userid
                    string usrkey = User.Identity.GetUserId();
                    if (dtoEvent.id != 0)
                    {
                        var Data = dbcontext.mblist_events_detail.Find(dtoEvent.id);
                        if (Data != null)
                        {
                            Data.event_detail_category_key = dtoEvent.Category;
                            Data.event_detail_type_key = dtoEvent.EventFor;
                            Data.event_detail_title = dtoEvent.Title;
                            Data.event_detail_address = dtoEvent.Address;
                            Data.event_detail_discription = dtoEvent.Comment;
                            Data.event_detail_date = invitationDate;
                            Data.event_detail_user_key = usrkey;
                            dbcontext.SaveChanges();
                            return Json(new { key = true, value = "event updated successfully", eventKey = Data.event_detail_key }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { key = false, value = "event event not found" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        mblist_events_detail evn = new mblist_events_detail()
                        {

                            event_detail_category_key = dtoEvent.Category,
                            event_detail_type_key = dtoEvent.EventFor,
                            event_detail_title = dtoEvent.Title,
                            event_detail_address = dtoEvent.Address,
                            event_detail_date = invitationDate,
                            event_detail_discription = dtoEvent.Comment,
                            event_detail_user_key = usrkey

                        };
                        dbcontext.mblist_events_detail.Add(evn);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "event added successfully", eventKey = evn.event_detail_key }, JsonRequestBehavior.AllowGet);
                    }
                };
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to save the event" }, JsonRequestBehavior.AllowGet); ;
            }
        }

        [HttpGet]
        public ActionResult SendInvitations(int eventId)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var events = dbcontext.mblist_events_detail.Find(eventId);
                    if (events != null)
                    {
                        var guests = dbcontext.mblist_event_guests.Where(x => x.guest_event_key == eventId);
                        if (guests.Any())
                        {
                            foreach (var guest in guests)
                            {
                                var userInfor = dbcontext.mblist_user_info.Where(x => x.usr_aspnet_user == guest.guest_user_key).FirstOrDefault();
                                if (userInfor != null)
                                {
                                    string username = userInfor.usr_first_name + " " + userInfor.usr_last_name;
                                    var type = dbcontext.mblist_type.Find(events.event_detail_type_key);
                                    string _type = "";
                                    if (type != null)
                                    {
                                        _type = type.type_name;
                                    }
                                    string datetime = events.event_detail_date.ToString("dd/MMM/yyyy hh:mm tt ");
                                    string body = PopulateBody(username, events.event_detail_title, datetime, events.event_detail_address, _type);
                                    string email = userInfor.AspNetUsers.Email;
                                    SendHtmlFormattedEmail(email, "MABRUKLIST INVITATION", body);
                                }
                            }
                            return Json(new { key = true, value = "Invitations send successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { key = false, value = "Unable to process your request.Please contact your admin" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Event not Found. Please contact your admin" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { key = false, value = "Unable to process your request. Please contact your admin" }, JsonRequestBehavior.AllowGet);
            }
        }

        private string PopulateBody(string userName, string EventTitle, string DateTime, string Address, string Type)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/TemplatesEmail/InvitationCard.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", userName);
            body = body.Replace("{EventTitle}", EventTitle);
            body = body.Replace("{DateTime}", DateTime);
            body = body.Replace("{Address}", Address);
            body = body.Replace("{type}", Type);
            return body;
        }

        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
            }
        }



        //SubEvent Mathod
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddSubevent(EventDTO sbevntdto)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    string usrkey = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    if (sbevntdto.id != 0)
                    {
                        mblist_events_detail sbven = new mblist_events_detail()
                        {
                            event_detail_user_key = usrkey,
                            event_parent = sbevntdto.id,
                            event_detail_category_key = sbevntdto.Category,
                            event_detail_type_key = sbevntdto.EventFor,
                            event_detail_title = sbevntdto.Title,
                            event_detail_address = sbevntdto.Address,
                            event_detail_date = Convert.ToDateTime(sbevntdto.Date),
                            event_detail_discription = sbevntdto.Comment
                        };
                        dbcontext.mblist_events_detail.Add(sbven);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Subevent added successfully", sbevntkey = sbven.event_detail_key }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { key = false, value = "Please create an event" }, JsonRequestBehavior.AllowGet);
                    }

                };
            }
            catch (Exception ex)
            {
                return Json(new { key = false, value = "Unable to process your request. Please contact your admin" }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpGet]
        public ActionResult VerifyContacts(int eventId)
        {
            List<ContactUsersDto> eventUsers = new List<ContactUsersDto>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                var allusers = dbcontext.mblist_event_guests.Where(x => x.guest_event_key == eventId);
                if (allusers.Any())
                {
                    eventUsers = allusers.AsEnumerable().Select(x => new ContactUsersDto
                    {
                        Id = dbcontext.mblist_user_info.Where(w => w.AspNetUsers.Id == x.guest_user_key).FirstOrDefault() != null ? (dbcontext.mblist_user_info.Where(w => w.AspNetUsers.Id == x.guest_user_key).FirstOrDefault().usr_key) : 0,
                        Users = dbcontext.mblist_user_info.Where(w => w.AspNetUsers.Id == x.guest_user_key).FirstOrDefault() != null ? ((dbcontext.mblist_user_info.Where(w => w.AspNetUsers.Id == x.guest_user_key).FirstOrDefault().mblist_title != null ? dbcontext.mblist_user_info.Where(w => w.AspNetUsers.Id == x.guest_user_key).FirstOrDefault().mblist_title.title_name + " " : string.Empty) + dbcontext.mblist_user_info.Where(w => w.AspNetUsers.Id == x.guest_user_key).FirstOrDefault().usr_first_name + " " + dbcontext.mblist_user_info.Where(w => w.AspNetUsers.Id == x.guest_user_key).FirstOrDefault().usr_last_name) : string.Empty,
                        Action = "<button type='button' class='btn btn-danger' onclick='Event.initRemoveContact(" + x.guest_key + ")'><i class='fa fa-close'></i> Remove Contact </button>"
                    }).ToList();
                }

            }
            return PartialView("_VerifyContacts", eventUsers);
        }

        [HttpGet]
        public ActionResult DeleteGuest(int guestKey)
        {
            try
            {
                using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
                {
                    var guest = dbcontext.mblist_event_guests.Find(guestKey);
                    if (guest != null)
                    {
                        dbcontext.mblist_event_guests.Remove(guest);
                        dbcontext.SaveChanges();
                        return Json(new { key = true, value = "Contact removed successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { key = false, value = "Contact not found or deleted" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { key = false, value = "Unable to process your request.Please contact your admin" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult InvitationTemplates()
        {
            List<InvitationDto> inviteList = new List<InvitationDto>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                inviteList = dbcontext.mblist_invitation_cards.AsEnumerable().OrderByDescending(x => x.inviation_key).Select(x => new InvitationDto
                {
                    Id = x.inviation_key,
                    path = x.invitation_img_path
                }).ToList();
            };
            return PartialView("_InvitationTemplates", inviteList);
        }

        [HttpGet]
        public ActionResult SetColorsModal(int eventId)
        {
            return PartialView("_SetColors");
        }

        [HttpPost]
        public ActionResult SubEventListing(int EventID)
        {
            List<SubEventDTO> list = new List<SubEventDTO>();
            using (MABRUKLISTEntities dbcontext = new MABRUKLISTEntities())
            {
                list = dbcontext.mblist_events_detail.Where(y=>y.event_parent != null && y.event_parent == EventID).AsEnumerable().OrderByDescending(x => x.event_detail_key).Select(x => new SubEventDTO
                {
                    eventID = x.event_detail_key,
                    Category = x.mblist_category.cat_name,
                    Type = x.mblist_type.type_name,
                    Title = x.event_detail_title,
                    EventDate = x.event_detail_date.ToString("dd/MMM/yyyy hh:mm tt "),
                    Address = x.event_detail_address
                }).ToList();
            };
            return PartialView("_SubEventListing",list);
        }

    }
}