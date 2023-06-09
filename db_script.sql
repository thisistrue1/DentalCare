USE [DentalCare]
GO
/****** Object:  Table [dbo].[tblAddress]    Script Date: 6/4/2023 1:59:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAddress](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NULL,
	[AddressLine3] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Zip] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBill]    Script Date: 6/4/2023 1:59:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBill](
	[BillId] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](9, 2) NOT NULL,
	[PaymentDueDate] [datetime] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastSentTime] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[BillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBillService]    Script Date: 6/4/2023 1:59:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBillService](
	[BillServiceId] [int] IDENTITY(1,1) NOT NULL,
	[BillId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_BillService] PRIMARY KEY CLUSTERED 
(
	[BillServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblInsuranceProvider]    Script Date: 6/4/2023 1:59:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblInsuranceProvider](
	[InsuranceProviderId] [int] IDENTITY(1,1) NOT NULL,
	[ProviderName] [varchar](50) NOT NULL,
	[AddressId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED 
(
	[InsuranceProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPatient]    Script Date: 6/4/2023 1:59:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPatient](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [varchar](50) NOT NULL,
	[AddressId] [int] NULL,
	[InsuranceProviderId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblService]    Script Date: 6/4/2023 1:59:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblService](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[ServiceDate] [datetime] NOT NULL,
	[ServiceCode] [varchar](50) NOT NULL,
	[ServiceDescription] [varchar](200) NULL,
	[Amount] [decimal](9, 2) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblAddress] ADD  CONSTRAINT [DF_Address_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblAddress] ADD  CONSTRAINT [DF_Address_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblAddress] ADD  CONSTRAINT [DF_Address_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblBill] ADD  CONSTRAINT [DF_tblBill_IsPaid]  DEFAULT ((0)) FOR [IsPaid]
GO
ALTER TABLE [dbo].[tblBill] ADD  CONSTRAINT [DF_Bill_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblBill] ADD  CONSTRAINT [DF_Bill_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblBill] ADD  CONSTRAINT [DF_Bill_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblBillService] ADD  CONSTRAINT [DF_BillService_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblBillService] ADD  CONSTRAINT [DF_BillService_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblBillService] ADD  CONSTRAINT [DF_BillService_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblInsuranceProvider] ADD  CONSTRAINT [DF_Provider_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblInsuranceProvider] ADD  CONSTRAINT [DF_Provider_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblInsuranceProvider] ADD  CONSTRAINT [DF_Provider_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblPatient] ADD  CONSTRAINT [DF_Patient_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblPatient] ADD  CONSTRAINT [DF_Patient_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblPatient] ADD  CONSTRAINT [DF_Patient_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblService] ADD  CONSTRAINT [DF_tblService_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblService] ADD  CONSTRAINT [DF_tblService_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblService] ADD  CONSTRAINT [DF_tblService_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblBillService]  WITH CHECK ADD  CONSTRAINT [FK_BillService_Bill] FOREIGN KEY([BillId])
REFERENCES [dbo].[tblBill] ([BillId])
GO
ALTER TABLE [dbo].[tblBillService] CHECK CONSTRAINT [FK_BillService_Bill]
GO
ALTER TABLE [dbo].[tblBillService]  WITH CHECK ADD  CONSTRAINT [FK_BillService_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[tblService] ([ServiceId])
GO
ALTER TABLE [dbo].[tblBillService] CHECK CONSTRAINT [FK_BillService_Service]
GO
ALTER TABLE [dbo].[tblInsuranceProvider]  WITH CHECK ADD  CONSTRAINT [FK_InsuranceProvider_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[tblAddress] ([AddressId])
GO
ALTER TABLE [dbo].[tblInsuranceProvider] CHECK CONSTRAINT [FK_InsuranceProvider_Address]
GO
ALTER TABLE [dbo].[tblPatient]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[tblAddress] ([AddressId])
GO
ALTER TABLE [dbo].[tblPatient] CHECK CONSTRAINT [FK_Patient_Address]
GO
ALTER TABLE [dbo].[tblPatient]  WITH CHECK ADD  CONSTRAINT [FK_Patient_InsuranceProvider] FOREIGN KEY([InsuranceProviderId])
REFERENCES [dbo].[tblInsuranceProvider] ([InsuranceProviderId])
GO
ALTER TABLE [dbo].[tblPatient] CHECK CONSTRAINT [FK_Patient_InsuranceProvider]
GO
ALTER TABLE [dbo].[tblService]  WITH CHECK ADD  CONSTRAINT [FK_Service_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[tblPatient] ([PatientId])
GO
ALTER TABLE [dbo].[tblService] CHECK CONSTRAINT [FK_Service_Patient]
GO
