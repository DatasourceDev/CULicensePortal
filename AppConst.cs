using CULCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CULCS
{
    public class LanguageLabel
    {
        public static string GetLabel(IQueryable<Language> languages, string key)
        {
            return languages.Where(w => w.LanguageKey == key).Select(s => s.LanguageValue).FirstOrDefault();
        }
    }
    public class AppConst
    {

    }
    public enum UserType
    {
        User = 0,
        Admin = 1,
        SuperAdmin = 2,
    }
    public enum PeriodType
    {
        Day,
        Month,
        Year
    }
    public enum Status
    {
        Enable = 0,
        Disable = 1,
        Invalid = 2,
    }
    public enum BorrowStatus
    {
        Borrowing,
        Returned,
        Expired,
        Cancelled,
        Advance,
    }

    public enum ReturnCode
    {
        Success = 1,
        Error = -1,
    }
    public enum userAccountControl
    {
        Enable = 544,
        Disable = 546,
    }

    public enum RegisterType
    {
        Manual,
        Import
    }
    public enum SendMessageType
    {
        SMS,
        Email
    }
    public enum LanguageCode
    {
        Thai = 0,
        English = 1,
        Chinese = 2,
    }
    public enum Page
    {
        Login = 1,
        Register = 2,
        RegisterOTP = 3,
        RegisterCompleted = 4,
        ForgotPassword = 5,
        ForgotPassword2 = 6,
        ForgotPasswordOTP = 7,
        ResetPassword = 8,
        ResetPasswordCompleted = 9,
        StudentProfile = 10,
        StaffProfile = 11,
        GuestProfile = 12,
        MacAddress = 13,
    }
    public static class ReturnMessage
    {
        public static string Success = "บันทึกข้อมูลสำเร็จ";
        public static string ChangePasswordSuccess = "เปลี่ยนรหัสผ่านสำเร็จ";
        public static string Error = "บันทึกข้อมูลไม่สำเร็จ";
        public static string ChangePasswordFail = "เปลี่ยนรหัสผ่านไม่สำเร็จ";
        public static string SuccessOTP = "ส่งรหัส OTP สำเร็จ";
        public static string DataInUse = "ไม่สามารถลบรายการน้ได้เนื่องจากมีข้อมูลบางรายการที่อ้างอิงถึงรายการนี้";

    }
    public static class LogActivity
    {
        public static string ResetPassword = "Reset Password";
        public static string ChangePassword = "Change Password";
        public static string RegisterGuest = "Register Guest";
        public static string ChangeExpiryDate = "Change Expiry Date";
        public static string ApproveGuest = "Approve Guest";
        public static string RejectGuest = "Reject Guest";
        public static string EditGuest = "Edit Guest";
        public static string DeleteGuest = "Delete Guest";
        public static string ImportGuest = "Import Guest";
        public static string OTPRequest = "OTP Requested";
        public static string OTPVerified = "OTP Verified";
        public static string DisableGuest = "Disable Guest";
        public static string EnableGuest = "Enable Guest";


    }
    public static class EnumStatus
    {
        public static UserType ToUserType(this string text)
        {
            UserType status = UserType.User;
            switch (text)
            {
                case "User":
                    status = UserType.User;
                    break;
                case "Admin":
                    status = UserType.Admin;
                    break;
                case "SuperAdmin":
                    status = UserType.SuperAdmin;
                    break;
                case "ผู้ใช้งาน":
                    status = UserType.User;
                    break;
                case "ผู้ดูแลระบบ":
                    status = UserType.Admin;
                    break;
                case "ผู้ดูและระบบพิเศษ":
                    status = UserType.SuperAdmin;
                    break;
                default:
                    break;
            }
            return status;
        }

        public static string toUserTypeName(this UserType statusType)
        {
            string status = "";
            switch (statusType)
            {
                case UserType.User:
                    status = "User";
                    break;
                case UserType.Admin:
                    status = "Admin";
                    break;
                case UserType.SuperAdmin:
                    status = "SuperAdmin";
                    break;
                default:
                    break;
            }
            return status;
        }

        public static Status toStatus(this string text)
        {
            var status = Status.Enable;
            switch (text)
            {
                case "Disable":
                    status = Status.Disable;
                    break;
                case "Enable":
                    status = Status.Enable;
                    break;
                case "Invalid":
                    status = Status.Invalid;
                    break;
                default:
                    break;
            }
            return status;
        }

        public static string toStatusName(this Status statusType)
        {
            string status = "";
            switch (statusType)
            {
                case Status.Disable:
                    status = "Disable";
                    break;
                case Status.Enable:
                    status = "Enable";
                    break;
                case Status.Invalid:
                    status = "Invalid";
                    break;
                default:
                    break;
            }
            return status;
        }

        public static BorrowStatus toBorrowStatus(this string text)
        {
            var status = BorrowStatus.Borrowing;
            switch (text)
            {
                case "Borrowed":
                    status = BorrowStatus.Borrowing;
                    break;
                case "Returned":
                    status = BorrowStatus.Returned;
                    break;
                case "Expired":
                    status = BorrowStatus.Expired;
                    break;
                case "Canceled":
                    status = BorrowStatus.Cancelled;
                    break;
                case "Advance":
                    status = BorrowStatus.Advance;
                    break;
                default:
                    break;
            }
            return status;
        }

        public static string toBorrowStatusName(this BorrowStatus statusType)
        {
            string status = "";
            switch (statusType)
            {
                case BorrowStatus.Borrowing:
                    status = "Borrowed";
                    break;
                case BorrowStatus.Returned:
                    status = "Returned";
                    break;
                case BorrowStatus.Expired:
                    status = "Expired";
                    break;
                case BorrowStatus.Cancelled:
                    status = "Canceled";
                    break;
                case BorrowStatus.Advance:
                    status = "Advance";
                    break;
                default:
                    break;
            }
            return status;
        }

        
     
    }
      
}
