using DVDShops.Models;
using DVDShops.Services.Producers;
using DVDShops.Services.Suppliers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DVDShops.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/provider")]
    public class ProviderController : Controller
    {
        private IProducerService producerService;
        private ISupplierService supplierService;
        public ProviderController(IProducerService producerService, ISupplierService supplierService)
        {
            this.producerService = producerService;
            this.supplierService = supplierService;
        }
        private void SetTempData(bool status, string title, string array)
        {
            TempData["status"] = status.ToString();
            TempData["title"] = title;
            TempData["message"] = array;
        }

        private void SetSupplier(long supplierId, string supplierName, string supplierEmail)
        {
            TempData["supplierId"] = supplierId;
            TempData["supplierOldName"] = supplierName;
            TempData["supplierOldEmail"] = supplierEmail;
        }

        [Route("view")]
        [Route("")]
        public IActionResult ProviderView()
        {
            ViewBag.ProducerList = producerService.GetAll();
            ViewBag.SupplierList = supplierService.GetAll();
            return View("ProviderView");
        }

        [Route("addProducer")]
        [HttpPost]
        public IActionResult AddProducer(Producer producer)
        {
            if (string.IsNullOrWhiteSpace(producer.ProducerName) || string.IsNullOrWhiteSpace(producer.ProducerIntroduction))
            {
                SetTempData(false, "Create Producer Failed!", "Some Input Field Is White Space Only!");
                return RedirectToAction("view", producer);
            }

            if (producerService.GetByName(producer.ProducerName) != null)
            {
                SetTempData(false, "Create Producer Failed!", "Producer Is Already Existed!");
                return RedirectToAction("view", producer);
            }

            var newProducer = new Producer
            {
                ProducerIntroduction = producer.ProducerIntroduction,
                ProducerName = producer.ProducerName,
                DeleteStatus = false,
            };

            var result = producerService.Create(producer);
            if (result)
            {
                SetTempData(true, "Create Producer Success!", "New Producer Just Added!");
            }
            else
            {
                SetTempData(false, "Create Producer Failed!", "Something Wrong!");
            }

            return RedirectToAction("view");
        }

        [Route("deleteProducer")]
        [HttpGet]
        public IActionResult DeleteProducer(int producerId)
        {
            var producer = producerService.GetById(producerId);
            producer.DeleteStatus = !producer.DeleteStatus;

            if (producerService.Update(producer))
            {
                if (producer.DeleteStatus)
                {
                    SetTempData(true, "Delete Producer Success!", $"{producer.ProducerName} Won't Appear On Website From Now!");
                }
                else
                {
                    SetTempData(true, "Recover Producer Success!", $"{producer.ProducerName} Will Appear On Website From Now!");
                }
            }
            else
            {
                SetTempData(false, "Delete Producer Failed!", "Something Wrong!");
            }

            return RedirectToAction("view");
        }

        [Route("FindProducerById")]
        [HttpGet]
        public IActionResult FindProducerById(int producerId)
        {
            var producer = producerService.GetById(producerId);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            return Json(producer, options);
        }

        [Route("editProducer")]
        [HttpPost]
        public IActionResult EditProducer(Producer producer, string oldName)
        {
            if (string.IsNullOrWhiteSpace(producer.ProducerName) || string.IsNullOrWhiteSpace(producer.ProducerIntroduction))
            {
                SetTempData(false, "Update Producer Failed!", "Some Input Field Is White Space Only!");
                return RedirectToAction("view", producer);
            }

            if (oldName.ToLower() != producer.ProducerName.ToLower().Trim())
            {
                if (producerService.GetByName(producer.ProducerName) != null)
                {
                    SetTempData(false, "Update Producer Failed!", "This Producer Is Already Existed!");
                    return RedirectToAction("view");
                }
            }

            if (producerService.Update(producer))
            {
                SetTempData(true, "Update Producer Success!", "Producer's Profile Updated!!");
            }
            return RedirectToAction("view");
        }

        [Route("addSupplier")]
        [HttpGet]
        public IActionResult AddSupplier()
        {
            return View("SupplierProfile");
        }

        [Route("addSupplier")]
        [HttpPost]
        public IActionResult AddSupplier(Supplier supplier, string phone)
        {
            long parsedPhone;
            if (long.TryParse(phone, out parsedPhone))
            {
                if (parsedPhone < 999999999)
                {
                    SetTempData(false, "Create Supplier Failed!", "Invalid Phone Number!");
                    return View("SupplierProfile", supplier);
                }
                supplier.SupplierPhone = parsedPhone;
            }
            else
            {
                SetTempData(false, "Create Supplier Failed!", "Invalid Phone Number!");
                return View("SupplierProfile", supplier);
            }

            if (string.IsNullOrWhiteSpace(supplier.SupplierName) ||
                string.IsNullOrWhiteSpace(supplier.SupplierAddress) ||
                string.IsNullOrWhiteSpace(supplier.SupplierEmail))
            {
                SetTempData(false, "Create Supplier Failed!", "Some Input Field Is White Space Only!");
                return View("SupplierProfile", supplier);
            }

            if (supplierService.GetByName(supplier.SupplierName) != null)
            {
                SetTempData(false, "Create Supplier Failed!", "Supplier Is Already Existed!");
                return View("SupplierProfile", supplier);
            }

            if (supplierService.GetByEmail(supplier.SupplierEmail) != null)
            {
                SetTempData(false, "Create Supplier Failed!", "Supplier Email Is Already Existed!");
                return View("SupplierProfile", supplier);
            }

            var newSupplier = new Supplier
            {
                SupplierName = supplier.SupplierName,
                SupplierEmail = supplier.SupplierEmail,
                SupplierPhone = supplier.SupplierPhone,
                SupplierAddress = supplier.SupplierAddress,
                DeleteStatus = false
            };

            if (supplierService.Create(supplier))
            {
                SetTempData(true, "Create Supplier Success!", "New Supplier Just Added!");
            }

            return RedirectToAction("view");
        }

        [Route("editSupplier")]
        [HttpGet]
        public IActionResult EditSupplier(int supplierId)
        {
            var supplier = supplierService.GetById(supplierId);
            SetSupplier(supplier.SupplierId, supplier.SupplierName, supplier.SupplierEmail);
            return View("SupplierProfile", supplier);
        }

        [Route("editSupplier")]
        [HttpPost]
        public IActionResult EditSupplier(Supplier supplier, string oldName, string oldEmail, string phone)
        {
            long parsedPhone;
            if (long.TryParse(phone, out parsedPhone))
            {
                if (parsedPhone < 999999999)
                {
                    SetTempData(false, "Create Supplier Failed!", "Invalid Phone Number!");
                    SetSupplier(supplier.SupplierId, supplier.SupplierName, supplier.SupplierEmail);
                    return View("SupplierProfile", supplier);
                }
                supplier.SupplierPhone = parsedPhone;
            }
            else
            {
                SetTempData(false, "Create Supplier Failed!", "Invalid Phone Number!");
                SetSupplier(supplier.SupplierId, supplier.SupplierName, supplier.SupplierEmail);
                return View("SupplierProfile", supplier);
            }

            if (string.IsNullOrWhiteSpace(supplier.SupplierName) ||
                string.IsNullOrWhiteSpace(supplier.SupplierAddress) ||
                string.IsNullOrWhiteSpace(supplier.SupplierEmail))
            {
                SetTempData(false, "Update Supplier Failed!", "Some Input Field Is White Space Only!");
                SetSupplier(supplier.SupplierId, supplier.SupplierName, supplier.SupplierEmail);
                return View("SupplierProfile", supplier);
            }

            if (oldName.ToLower() != supplier.SupplierName.ToLower().Trim())
            {
                if (supplierService.GetByName(supplier.SupplierName) != null)
                {
                    SetTempData(false, "Update Supplier Failed!", "Supplier Is Already Existed!");
                    SetSupplier(supplier.SupplierId, supplier.SupplierName, supplier.SupplierEmail);
                    return View("SupplierProfile", supplier);
                }
            }

            if (oldEmail.ToLower() != supplier.SupplierEmail.ToLower().Trim())
            {
                if (supplierService.GetByEmail(supplier.SupplierEmail) != null)
                {
                    SetTempData(false, "Update Supplier Failed!", "Supplier Email Is Already Existed!");
                    SetSupplier(supplier.SupplierId, supplier.SupplierName, supplier.SupplierEmail);
                    return View("SupplierProfile", supplier);
                }
            }

            if (supplierService.Update(supplier))
            {
                SetTempData(true, "Update Supplier Success!", "Supplier's Profile Updated!!");
            }
            else
            {
                SetTempData(false, "Update Supplier Wrong!", "Wrong Wrong!");
            }
            return RedirectToAction("view");
        }

        [Route("deleteSupplier")]
        [HttpGet]
        public IActionResult DeleteSupplier(int supplierId)
        {
            var supplier = supplierService.GetById(supplierId);
            supplier.DeleteStatus = !supplier.DeleteStatus;

            if (supplierService.Update(supplier))
            {
                if (supplier.DeleteStatus)
                {
                    SetTempData(true, "Delete Supplier Success!", $"{supplier.SupplierName} Won't Appear On Website From Now!");
                }
                else
                {
                    SetTempData(true, "Recover Supplier Success!", $"{supplier.SupplierName} Will Appear On Website From Now!");
                }
            }
            else
            {
                SetTempData(false, "Delete Supplier Failed!", "Something Wrong!");
            }

            return RedirectToAction("view");
        }
    }
}
